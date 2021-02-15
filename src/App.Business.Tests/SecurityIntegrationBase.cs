using System;
using App.Data.Contexts;
using Core.Data;
using Core.Data.Helpers;
using Core.Models;
using Core.Services;
using Core.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace App.Business.Tests
{
    public static class SecurityIntegrationBase
    {
        public static readonly IConfigurationRoot Configuration;
        private static readonly string _connection;
        private static readonly IQueryTextService _queryTextService;
        private static readonly IQueryConditionsBuilder _queryConditionsHelper;
        private static readonly IObjectMapper _mapper;

        static SecurityIntegrationBase()
        {
            var loggerMock = new Mock<ILogger<PostgresQueryTextService>>();

            _queryConditionsHelper = new PostgresQueryConditionsBuilder();
            _queryTextService = new PostgresQueryTextService(_queryConditionsHelper, loggerMock.Object);
            _mapper = new ObjectMapper();

            #region Db

            var builder = new ConfigurationBuilder();

            if (Environment.GetEnvironmentVariable("INTEGRATION_TEST_ENVIRONMENT") != null)
            {
                builder.AddJsonFile(Environment.CurrentDirectory + "/../../../appsettings.global.json", optional: false, reloadOnChange: false);
                Console.WriteLine("Run tests with appsettings.global.json");
            }
            else
            {
                builder.AddJsonFile(Environment.CurrentDirectory + "/../../../appsettings.tests.json", optional: false, reloadOnChange: false);
                Console.WriteLine("Run tests with appsettings.tests.json");
            }

            Configuration = builder.Build();
            _connection = Configuration.GetConnectionString("Security");

            var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connection).Options, new AuditEntityEntryChangesTracker());

            //if (context.Database.EnsureCreated())
            //{
            //    DbInitializerForTestDb.Initialize(context, configuration);
            //}
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            // do not delete database as for now to be able look at the db if tests fail
            //context.Database.EnsureDeleted();
            #endregion
        }

        public static AppDbContext CreateContextInstance()
        {
            var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connection).Options, new AuditEntityEntryChangesTracker());
            return context;
        }

        public static ICommonDataService CreateDataServiceInstance(BaseUserInfo user)
        {
            var userServiceMock = new Mock<IUserInfoService>();
            userServiceMock.Setup(x => x.GetCurrentUserInfoAsync())
                .ReturnsAsync(user);
            userServiceMock.Setup(x => x.GetCurrentUserInfo())
                .Returns(user);

            var pendingChangeServiceMock = new Mock<IPendingChangeService>();
            pendingChangeServiceMock.SetReturnsDefault<PendingChange>(null);

            var dataService = new CommonDataService(CreateContextInstance(), _queryTextService, null, userServiceMock.Object, pendingChangeServiceMock.Object, _mapper, null);
            return dataService;
        }

        public static IObjectMapper GetMapper()
        {
            return _mapper;
        }
    }
}
