using System;
using Core.Data;
using Core.Data.Helpers;
using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace App.Data.Helpers
{
    public class AppDbConfigurationProvider<TContext>: DbConfigurationProvider<TContext> where TContext : CoreDbContext, IApplicationSettingsContext
    {
        public AppDbConfigurationProvider(ILogger<DbConfigurationProvider<TContext>> logger, Action<DbContextOptionsBuilder> options, IAuditEntityEntryChangesTracker auditTracker)
            : base(logger, options, auditTracker)
        {
            ChangeToken.OnChange(() => ConfigurationChangeHelper.ApplicationDbSettingsToken, Load);
        }

        public AppDbConfigurationProvider(ILogger<DbConfigurationProvider<TContext>> logger, Action<DbContextOptionsBuilder> options) 
            : base(logger, options)
        {
            ChangeToken.OnChange(() => ConfigurationChangeHelper.ApplicationDbSettingsToken, Load);
        }

        public AppDbConfigurationProvider(Action<DbContextOptionsBuilder> options, IAuditEntityEntryChangesTracker auditTracker)
           : base(options, auditTracker)
        {
            ChangeToken.OnChange(() => ConfigurationChangeHelper.ApplicationDbSettingsToken, Load);
        }

        public AppDbConfigurationProvider(Action<DbContextOptionsBuilder> options)
            : base(options)
        {
            ChangeToken.OnChange(() => ConfigurationChangeHelper.ApplicationDbSettingsToken, Load);
        }
    }
}
