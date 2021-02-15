using System;
using Core.Data;
using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddAppDbProvider<TContext>(
            this IConfigurationBuilder configuration, Type providerType, Action<DbContextOptionsBuilder> setup) where TContext : CoreDbContext
        {
            configuration.Add(new AppDbConfigurationSource<TContext>(setup, providerType));
            return configuration;
        }

        public static IConfigurationBuilder AddAppDbProvider<TContext>(
            this IConfigurationBuilder configuration, Type providerType, Action<DbContextOptionsBuilder> setup, IAuditEntityEntryChangesTracker auditTracker) where TContext : CoreDbContext
        {
            configuration.Add(new AppDbConfigurationSource<TContext>(setup, providerType, auditTracker));
            return configuration;
        }
    }
}
