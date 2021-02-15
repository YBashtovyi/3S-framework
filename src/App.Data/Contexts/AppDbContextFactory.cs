using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace App.Data.Contexts
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var basePath = Environment.GetEnvironmentVariable("DEPLOY_LOCATION").Equals("Local") ?
                Directory.GetParent(Environment.CurrentDirectory).ToString() + Path.DirectorySeparatorChar + "App.Api" :
                Environment.CurrentDirectory;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new AppDbContext(builder.Options, null);
        }
    }
}
