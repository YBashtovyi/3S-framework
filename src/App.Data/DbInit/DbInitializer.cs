using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using App.Data.Contexts;
using App.Data.Enums;
using App.Data.Models;
using Core.Common.Enums;
using Core.Data;
using Core.Data.Common;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Npgsql;


namespace App.Data.DbInit
{
    public class DbInitializer
    {
        private bool IsDevelopment { get; set; } = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development").ToLower().Contains("development");
        private readonly ILoggerFactory _loggerFactory;
        private readonly bool _seedEhealthData = false;
        private ILogger<DbInitializer> Logger
        {
            get
            {
                return _loggerFactory.CreateLogger<DbInitializer>();
            }
        }

        public DbInitializer(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public async Task<bool> InitializeApiContextAsync(AppDbContext context, IConfiguration configuration)
        {
            EnsureCreated(context);
            
            //var region = new Region();
            //region.Id = Guid.NewGuid();
            //region.Code = "3200000000";
            //region.Name = "Київська область";
            //region.KOATU = "3200000000";
            //region.AtuRegionType = "dev";

            //await context.Region.AddAsync(region);
            //await context.SaveChangesAsync();

            return true;

            //try
            //{
            //    ExecuteMigrationCommands(configuration);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error($"Failed to execute some seeding scrips because of exception: {ex.InnerException?.Message ?? ex.Message}");
            //}

            //var exists = context.EnumRecord.Any();
            //if (exists)
            //{
            //    return exists; // DB has been seeded
            //}

            //AddExPropertiesToContext(context);
            //await context.SaveChangesAsync();
            //try
            //{
            //    SeedApiApplicationSettings(context);
            //}
            //catch (Exception ex)
            //{
            //    Logger.LogError(ex, "Failed to seed apiApplication settings because of exception: {0}", ex.InnerException?.Message ?? ex.Message);
            //}
            //if (IsDevelopment)
            //{
            //    await SeedDevelopmentEnvironmentAsync(context, configuration);
            //}
            //return exists;
        }


        private bool EnsureCreated(AppDbContext context)
        {
            var creatingDbAttemptsCount = 0;
            while (true)
            {
                creatingDbAttemptsCount++;
                try
                {
                    // should use migrations
                    // uncomment if want to use database from scratch and comment getting EnumRecord
                    //var result = context.Database.EnsureCreated();
                    var result = context.EnumRecord.Any();
                    return result;
                }
                catch (Exception ex)
                {
                    if (creatingDbAttemptsCount >= 3)
                    {
                        Logger.LogError(ex, "Failed to initialize database during starting: {0}", ex.InnerException?.Message ?? ex.Message);
                        throw;
                    }
                    Thread.Sleep(5000);
                }
            }
        }
    }
}
