using System;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Logging;
using Core.Services;

namespace Core.Data
{
    public class DbConfigurationProvider<TContext>: ConfigurationProvider where TContext : CoreDbContext, IApplicationSettingsContext
    {
        protected readonly Action<DbContextOptionsBuilder> Options;
        protected readonly ILogger Logger;
        private readonly IAuditEntityEntryChangesTracker _auditTracker;

        public DbConfigurationProvider(ILogger<DbConfigurationProvider<TContext>> logger, Action<DbContextOptionsBuilder> options, IAuditEntityEntryChangesTracker auditTracker)
        {
            Options = options;
            Logger = logger;
            _auditTracker = auditTracker;
        }

        public DbConfigurationProvider(Action<DbContextOptionsBuilder> options, IAuditEntityEntryChangesTracker auditTracker)
        {
            Options = options;
            _auditTracker = auditTracker;
        }

        public DbConfigurationProvider(ILogger<DbConfigurationProvider<TContext>> logger, Action<DbContextOptionsBuilder> options)
        {
            Options = options;
            Logger = logger;
        }

        public DbConfigurationProvider(Action<DbContextOptionsBuilder> options)
        {
            Options = options;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            Options(builder);

            try
            {
                using (var context =
                    (TContext)Activator.CreateInstance(typeof(TContext), _auditTracker == null ? new object[] { builder.Options } : new object[] { builder.Options, _auditTracker }))
                {
                    // uncomment this if do not use migrations and database will be created after execution this method
                    //context.Database.EnsureCreated();

                    var items = from setting in context.ApplicationSetting
                                where setting.IsEnabled
                                select new { setting.Name, setting.Value };

                    Data.Clear();
                    foreach (var item in items)
                    {
                        Data.Add(item.Name, item.Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Cannot load configuration data from context of type {0}", typeof(TContext));
            }
        }
    }
}
