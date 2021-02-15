using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Core.Models;
using Core.Services;

namespace Core.Data
{
    public class AppDbConfigurationSource<TContext>: IConfigurationSource where TContext: CoreDbContext
    {
        private readonly Action<DbContextOptionsBuilder> _optionsAction;
        private readonly Type _providerType;
        private readonly IAuditEntityEntryChangesTracker _auditTracker;

        public AppDbConfigurationSource(Action<DbContextOptionsBuilder> optionsAction, Type providerType)
        {
            _optionsAction = optionsAction;
            _providerType = providerType;
        }

        public AppDbConfigurationSource(Action<DbContextOptionsBuilder> optionsAction, Type providerType, IAuditEntityEntryChangesTracker auditTracker)
        {
            _optionsAction = optionsAction;
            _providerType = providerType;
            _auditTracker = auditTracker;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return _auditTracker == null
                ? Activator.CreateInstance(_providerType, _optionsAction) as IConfigurationProvider
                : Activator.CreateInstance(_providerType, _optionsAction, _auditTracker) as IConfigurationProvider;
        }
    }
}
