using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using App.Business.Filters;
using App.Business.IdentityServer;
using App.Business.Services.AdministrationServices;
using App.Business.Services.ApiControllerServices;
using App.Business.Services.ApplicationServices;
using App.Business.Services.CdnServices;
using App.Business.Services.ConstructionObjectServices;
using App.Business.Services.PrjServices;
using App.Business.Services.OrganizationServices;
using App.Business.Services.CityServices;
using App.Business.Services.CountryServices;
using App.Data.Contexts;
using App.WebAPI.Middlewares;
using Core.Administration.Models;
using Core.Business.Services;
using Core.Data.Helpers;
using Core.Models;
using Core.Services;
using Core.Services.CorrelationId;
using Core.Services.Data;
using Core.Services.DistributedCacheService;
using Core.ThirdParty.Redis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
//using Prometheus;

namespace App.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }
        private readonly ILoggerFactory _loggerFactory;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddResponseCaching();
            services.AddMemoryCache();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<MetricReporter>();

            var audience = Environment.GetEnvironmentVariable("ID_JWT_BEARER_AUDIENCE") ?? Configuration["Identity:JwtBearerAudience"];
            var audiences = audience?.Split(';');

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = "smart";
            })
                .AddPolicyScheme("smart", "Bearer or Jwt", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        var bearerAuth =
                            context.Request.Headers["Authorization"].FirstOrDefault()?.StartsWith("Bearer ") ?? false;
                        return bearerAuth
                            ? JwtBearerDefaults.AuthenticationScheme
                            : CookieAuthenticationDefaults.AuthenticationScheme;
                    };
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(jwt =>
                {
                    jwt.Authority = Environment.GetEnvironmentVariable("ID_AUTHORITY") ?? Configuration["Identity:Authority"];
                    jwt.RequireHttpsMetadata = false;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5),
                        RequireExpirationTime = true,
                        ValidAudiences = audiences
                    };
                });

            ConfigureCors(services);

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy =
                    new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme,
                            JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();
            });

            services.AddControllers(option => { option.Filters.Add<ApiAuthorizationFilter>(); })
                .AddNewtonsoftJson(opt => opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc);

            services.AddLocalization(opt => opt.ResourcesPath = "Resources");

            AddSwaggerWithOptions(services, Configuration);

            // Custom services and context
            AddCustomServices(services);
            AddAppDbContexts(services, Configuration);

            // for converting doc to pdf
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Use the Prometheus middleware
            //app.UseMetricServer();
            app.UseResponseTimeMiddleware();

            app.UseCors(GetCorsPolicyName());
            //app.UseHttpsRedirection();
            app.UseRouting();

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("uk"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("uk"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                //SupportedUICultures = supportedCultures
            });

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMvc();
            app.UseCorrelationId();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MIS API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureCors(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();
            var cors = Configuration.GetSection("Cors");

            var policyName = GetCorsPolicyName();
            var allowAnyOrigin = cors.GetValue<bool>("AllowAnyOrigin");
            var allowAnyMethod = cors.GetValue<bool>("AllowAnyMethod");
            var allowAnyHeader = cors.GetValue<bool>("AllowAnyHeader");
            var allowCredentials = cors.GetValue<bool>("AllowCredentials");
            var allowedOrigins = Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
            var allowedOrginsRepresantaion = allowedOrigins == null ? string.Empty : string.Join(", ", allowedOrigins);

            logger.LogInformation($@"CORS policy loaded. 
                PolicyName: {policyName}, 
                AllowAnyOrigin: {allowAnyOrigin}, 
                AllowAnyMethod: {allowAnyMethod},
                AllowAnyHeader: {allowAnyHeader},
                AllowCredentials: {allowCredentials},
                AllowedOrigins: {allowedOrginsRepresantaion}");

            if (!allowAnyOrigin && (allowedOrigins == null || allowedOrigins.Length == 0))
            {

                logger.LogWarning(@"CORS policy does not allow any origin and allowed origins are not set in the configuration.
Perhaps CORS policy is not set in the configuration. Check, that the proper appsettings file is loaded into configuration and the CORS sections does exist.
Also check that in configuration AllowAnyOrigin equals to true OR AllowedOrigins array is set");
                return;
            }

            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    builder =>
                    {
                        if (allowAnyOrigin)
                        {
                            builder.AllowAnyOrigin();
                        }
                        else
                        {
                            builder.WithOrigins(allowedOrigins);
                        }

                        if (allowAnyMethod)
                        {
                            builder.AllowAnyMethod();
                        }

                        if (allowAnyHeader)
                        {
                            builder.AllowAnyHeader();
                        }

                        if (allowCredentials)
                        {
                            builder.AllowCredentials();
                        }
                    });
            });
        }

        private string GetCorsPolicyName() => Configuration.GetValue<string>("Cors:PolicyName") ?? "Default";

        private void AddSwaggerWithOptions(IServiceCollection services, IConfiguration configuration)
        {
            var documentationFolders = configuration.GetSection("Swagger:DocumentationFileFolders").Get<string[]>() ?? new string[0];
            var documentationFiles = new List<string>();
            foreach (var folder in documentationFolders)
            {
                try
                {
                    var files = Directory.GetFiles(folder, "*.xml");
                    foreach (var file in files)
                    {
                        documentationFiles.Add(file);
                    }
                }
                catch (Exception ex)
                {
                    _loggerFactory.CreateLogger<Program>().LogWarning("Failed to read documentation files because of exception: {0}", ex.Message);
                }

            }
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "IPM WebAPI",
                    Description = "IPM WebAPI"
                });

                foreach (var path in documentationFiles)
                {
                    c.IncludeXmlComments(path);
                }
            });
        }

        private void AddCustomServices(IServiceCollection services)
        {
            // Redis
            services.AddSingleton<IRedisdatabaseProvider, RedisDatabaseProvider>();
            services.AddScoped<IDistributedCacheService, DistributedCacheService>();
            services.AddScoped<CacheValueService>();

            services.AddScoped<IQueryTextService, PostgresQueryTextService>();
            services.AddSingleton<IQueryConditionsBuilder, PostgresQueryConditionsBuilder>();
            services.AddScoped<IAuditEntityEntryChangesTracker, AuditEntityEntryChangesTracker>();
            services.AddScoped<IObjectMapper, ObjectMapper>();
            //services.AddScoped<IObjectMapper, MapsterMapper>();
            services.AddScoped<IQueryableCacheService, QueryableCacheService>();
            services.AddScoped<ICommonDataService, CommonDataService>();
            services.AddScoped<IOfficeDocumentService, XlsxService>();
            services.AddCorrelationId();

            services.AddScoped<IFileStoreDestination, FileStoreDestinationService>();
            services.AddScoped<IFileStoreService, FileStoreService>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddSingleton<MemoryCacheHelper>();
            services.AddScoped<IPendingChangeService, PendingChangeService>();
            services.AddScoped<SysEvaluatedValueService>();
            services.AddScoped<CommonDtoService>();

            services.AddSingleton<ISmtpClientService, SmtpClientService>();

            services.AddScoped<DefaultValuesService>();

            services.AddScoped<INumberCounterService, NumberCounterService>();

            services.AddScoped<OrganizationService>();
            services.AddScoped<OrgUnitPositionService>();
            services.AddScoped<OrgUnitStaffService>();

            services.AddScoped<UserService>();
            services.AddScoped<RoleService>();
            services.AddScoped<IdentityService>();
            services.AddScoped<ClientCredentialsManager>();
            services.AddScoped<IIdentityHttpClient, IdentityHttpClient>();

            services.AddScoped<CityService>();
            services.AddScoped<CountryService>();

            services.AddScoped<TokenService>();
            services.AddScoped<AuthService>();

            AddOneSignalServices(services);
            AddEhealthServices(services);
            AddControllerServices(services);
            AddIpmServices(services);
        }

        private static void AddOneSignalServices(IServiceCollection services)
        {
            services.AddHttpClient<IOneSignalHttpClient, OneSignalHttpClient>();
            services.AddScoped<IOneSignalService, OneSignalService>();
        }

        private static void AddEhealthServices(IServiceCollection services)
        {
            services.AddScoped<IPrintedFormService, PrintedFormService>();
        }

        private void AddControllerServices(IServiceCollection services)
        {
            services.AddScoped<AccountService>();
            services.AddScoped<EnumRecordService>();
            services.AddScoped<FileStoreControllerService>();
            services.AddScoped<NotificationService>();
        }

        private void AddIpmServices(IServiceCollection services)
        {
            services.AddScoped<ProjectService>();
            services.AddScoped<ProjectParticipantService>();
            services.AddScoped<ProjectContractService>();
            services.AddScoped<ProjectWorkScheduleService>();
            services.AddScoped<ProjectWorkScheduleStageService>();
            services.AddScoped<ProjectPhotoReportService>();
            services.AddScoped<ConstructionObjectService>();
            services.AddScoped<ConstructionObjectExPropertyDictionaryService>();
            services.AddScoped<WorkSubTypeService>();
        }

        private void AddAppDbContexts(IServiceCollection services, IConfiguration configuration)
        {
            var connection = Environment.GetEnvironmentVariable("PG_CONNECTION_STRING") ?? configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connection);
#if DEBUG
                options.EnableSensitiveDataLogging(true);
#endif
            });


            services.AddDbContext<CoreDbContext, AppDbContext>();
            services.AddDbContext<IAdministrationDbContext, AppDbContext>(options => options.UseNpgsql(connection));
        }
    }
}
