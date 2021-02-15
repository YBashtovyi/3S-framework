using System;
using System.Threading.Tasks;
using App.Api.Logging;
using App.Data.Contexts;
using App.Data.DbInit;
using App.Data.Helpers;
using Core.Data.Helpers;
using Core.Extensions;
using Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Loki;
using Serilog.Sinks.Elasticsearch;

namespace App.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //ConfigStore.Configuration = new ConfigurationBuilder()
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: false, reloadOnChange: true)
                .Build();

            ConfigureSerilog(config);
            var host = CreateWebHostBuilder(config, args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //if (bool.Parse(ConfigStore.Configuration.GetSection("SeedDB").Value))
                    if (bool.Parse(config.GetSection("SeedDB").Value))
                    {
                        var context = services.GetRequiredService<AppDbContext>();
                        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                        var alreadyExisted = await (new DbInitializer(loggerFactory)).InitializeApiContextAsync(context, config);
                        if (!alreadyExisted)
                        {
                            //EhealthPrescriptionsDbInitializer.InitializeApiContext(context);
                        }
                        // reloading appsettings from db to ICOnfiguration because db is just seeded
                        ConfigurationChangeHelper.ApplicationDbSettingsTokenOnReload();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                    throw;
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(IConfiguration config, string[] args) =>
            Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    var connectionString = Environment.GetEnvironmentVariable("PG_CONNECTION_STRING") ?? config.GetConnectionString("DefaultConnection");
                    var providerType = typeof(AppDbConfigurationProvider<AppDbContext>);
                    // TODO: mabe it is possible to use injection here instead of using new AuditEntityEntryChangesTracker()
                    builder.AddAppDbProvider<AppDbContext>(providerType, options => options.UseNpgsql(connectionString), new AuditEntityEntryChangesTracker());
                })
                .UseStartup<Startup>()
                .UseSerilog(Log.Logger)
                .UseUrls(config.GetValue<string>("Urls"));

        private static void ConfigureSerilog(IConfigurationRoot config)
        {
            var loggerConfiguration = new LoggerConfiguration()
                //.ReadFrom.Configuration(config);
                .Enrich.FromLogContext();

            // Don't touch this!!!                
            if ($"{Environment.GetEnvironmentVariable("LOG_LEVEL")}".ToLower() == "verbose")
            {   
                loggerConfiguration.MinimumLevel.Verbose();
            }
            else if ($"{Environment.GetEnvironmentVariable("LOG_LEVEL")}".ToLower() == "information")
            {
                loggerConfiguration.MinimumLevel.Information();
            }
            else if ($"{Environment.GetEnvironmentVariable("LOG_LEVEL")}".ToLower() == "warning")
            {
                loggerConfiguration.MinimumLevel.Warning();
            }
            else if ($"{Environment.GetEnvironmentVariable("LOG_LEVEL")}".ToLower() == "error")
            {
                loggerConfiguration.MinimumLevel.Error();
            }
            else 
            {
                // default log level
                loggerConfiguration.MinimumLevel.Warning();
#if DEBUG
                loggerConfiguration.MinimumLevel.Verbose();
#endif
            }

            if (Environment.GetEnvironmentVariable("LOG_TO") != null)
            {
                if ($"{Environment.GetEnvironmentVariable("LOG_TO")}".ToLower() == "loki")
                {
                    var lokiConnection = new NoAuthCredentials($"{Environment.GetEnvironmentVariable("LOKI_HOST")}");
                    loggerConfiguration.WriteTo.LokiHttp(lokiConnection, new LogLabelProvider());
                }

                else if ($"{Environment.GetEnvironmentVariable("LOG_TO")}".ToLower() == "elasticsearch")
                {
                    loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri($"{Environment.GetEnvironmentVariable("ELASTICSEARCH_HOST")}"))

                        {
                            IndexFormat = $"ipm-" + $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}".ToLower() + "-" + DateTime.Now.ToString("yyyy-MM-dd"),
                            DetectElasticsearchVersion = true, // Performs a call to detect ES 6 or ES 7
                            AutoRegisterTemplate = true,
                            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7 // Use ES 7
                        }
                    );
                }

            }

            // Always write to console
            loggerConfiguration.WriteTo.Console();

            Log.Logger = loggerConfiguration.CreateLogger();
        }
    }
}
