using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Core.Base.Data;
using Core.Data.Extensions;
using Core.Models;
using Core.Security.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Contexts
{
    public partial class AppDbContext: BaseDbContext, IApplicationSettingsContext
    {
        public AppDbContext() : base() { }

        public AppDbContext(DbContextOptions<AppDbContext> options, IAuditEntityEntryChangesTracker auditTracker) : base(options, auditTracker) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            BuildAtuModels(builder);
            BuildCommonModels(builder);
            BuildOrgModels(builder);
            BuildSystemModels(builder);
            BuildScheduleModels(builder);
            BuildProjectModels(builder);
            BuildCdnModels(builder);

            var useSnakeCase = true; // TODO: move to configuration
            if (useSnakeCase)
            {
                builder.UseSnakeCaseNaming();
            }

            // Administration
            BuildAdmModels(builder);
            IgnoreKeylessTypesWhenCreateTables(builder);
        }
    }
}
