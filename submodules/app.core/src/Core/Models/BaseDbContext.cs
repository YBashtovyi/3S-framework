using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Common.Extensions;
using Core.Data;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Models
{
    public class BaseDbContext: CoreDbContext, IAuditableContext
    {
        private readonly IAuditEntityEntryChangesTracker _auditTracker;

        #region Entities
        public DbSet<AuditHistory> AppAudit { get; set; }
        public DbSet<PendingChange> PendingChange { get; set; }
        #endregion

        /// <summary>
        /// Classes used in db set, where:
        /// key - type
        /// value - entity has key
        /// </summary>
        private static Dictionary<Type, bool> ApplicationModels;

        public BaseDbContext()
        {

        }

        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        public BaseDbContext(DbContextOptions options, IAuditEntityEntryChangesTracker auditTracker) : base(options)
        {
            _auditTracker = auditTracker;
        }

        public BaseDbContext(DbContextOptions<BaseDbContext> options, IAuditEntityEntryChangesTracker auditTracker) : base(options)
        {
            _auditTracker = auditTracker;
        }

        public override IEnumerable<Type> GetApplicationModels(bool withPrimaryKeysOnly)
        {
            if (ApplicationModels == null)
            {
                var sets = GetType().GetPropertyGenericArguments(typeof(DbSet<>));
                var entityTypes = Model.GetEntityTypes();
                var models = new Dictionary<Type, bool>();
                foreach (var dbSetClass in sets)
                {
                    var efType = entityTypes.FirstOrDefault(x => x.ClrType == dbSetClass);
                    if (efType != null)
                    {
                        if (efType.FindPrimaryKey() == null)
                        {
                            if (!models.ContainsKey(dbSetClass))
                            {
                                models.Add(dbSetClass, false);
                            }
                        }
                        else
                        {
                            models.Add(dbSetClass, true);
                        }
                    }
                }

                ApplicationModels = models;
            }

            foreach (var kv in ApplicationModels)
            {
                if (withPrimaryKeysOnly && !kv.Value)
                {
                    continue;
                }
                yield return kv.Key;
            }
        }

        #region OverrideDbContext
        public override EntityEntry Add(object entity)
        {
            if (entity is IEntity coreEntity)
            {
                if (coreEntity.Id == Guid.Empty)
                {
                    coreEntity.Id = Guid.NewGuid();
                }
            }
            return base.Add(entity);
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            if (entity is IEntity coreEntity)
            {
                if (coreEntity.Id == Guid.Empty)
                {
                    coreEntity.Id = Guid.NewGuid();
                }
            }
            return base.Add(entity);
        }

        public override void AddRange(IEnumerable<object> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is IEntity coreEntity)
                {
                    if (coreEntity.Id == Guid.Empty)
                    {
                        coreEntity.Id = Guid.NewGuid();
                    }
                }
            }

            base.AddRange(entities);
        }

        public override void AddRange(params object[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity is IEntity coreEntity)
                {
                    if (coreEntity.Id == Guid.Empty)
                    {
                        coreEntity.Id = Guid.NewGuid();
                    }
                }
            }
            base.AddRange(entities);
        }

        public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
        {
            if (entity is IEntity coreEntity)
            {
                if (coreEntity.Id == Guid.Empty)
                {
                    coreEntity.Id = Guid.NewGuid();
                }
            }
            return base.AddAsync(entity, cancellationToken);
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is IEntity coreEntity)
            {
                if (coreEntity.Id == Guid.Empty)
                {
                    coreEntity.Id = Guid.NewGuid();
                }
            }
            return base.AddAsync(entity, cancellationToken);
        }

        public override Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                if (entity is IEntity coreEntity)
                {
                    if (coreEntity.Id == Guid.Empty)
                    {
                        coreEntity.Id = Guid.NewGuid();
                    }
                }
            }
            return base.AddRangeAsync(entities, cancellationToken);
        }

        public override Task AddRangeAsync(params object[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity is IEntity coreEntity)
                {
                    if (coreEntity.Id == Guid.Empty)
                    {
                        coreEntity.Id = Guid.NewGuid();
                    }
                }
            }
            return base.AddRangeAsync(entities);
        }

        public override EntityEntry Update(object entity)
        {
            return base.Update(entity);
        }

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            return base.Update(entity);
        }

        public override void UpdateRange(IEnumerable<object> entities)
        {
            base.UpdateRange(entities);
        }

        public override void UpdateRange(params object[] entities)
        {
            base.UpdateRange(entities);
        }

        #endregion

        public virtual async Task<int> SaveAuditableAsync(BaseUserInfo user, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges(user);
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }

        public virtual async Task<int> SaveAuditableAsync(BaseUserInfo user, CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges(user);
            var result = await base.SaveChangesAsync(cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }

        public virtual int SaveAuditable(BaseUserInfo user, bool acceptAllChangesOnSuccess)
        {
            var auditEntries = OnBeforeSaveChanges(user);
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            OnAfterSaveChanges(auditEntries);
            return result;
        }

        public virtual int SaveAuditable(BaseUserInfo user)
        {
            var auditEntries = OnBeforeSaveChanges(user);
            var result = base.SaveChanges();
            OnAfterSaveChanges(auditEntries);
            return result;
        }

        private IEnumerable<AppAuditEntry> OnBeforeSaveChanges(BaseUserInfo user = null)
        {
            var authorId = GetCurrentUserId(user);
            var entries = ChangeTracker.Entries();
            foreach (var trackedEntry in entries)
            {
                if (trackedEntry.State == EntityState.Detached || trackedEntry.State == EntityState.Deleted)
                {
                    continue;
                }

                if (trackedEntry.Entity is ICoreEntity trackedCoreEntity)
                {
                    if (trackedEntry.State == EntityState.Added)
                    {
                        SetNewEntityFields(trackedCoreEntity, user);
                    }
                    else
                    {
                        UpdateEntityFields(trackedCoreEntity, user);
                    }
                }
            }

            if (_auditTracker == null)
            {
                return Enumerable.Empty<AppAuditEntry>();
            }

            ChangeTracker.DetectChanges();
            var auditEntries = _auditTracker.GetChanges(ChangeTracker.Entries(), authorId);

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(entry => !entry.HasTemporaryProperties))
            {
                AppAudit.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(entry => entry.HasTemporaryProperties);
        }

        private Task OnAfterSaveChanges(IEnumerable<AppAuditEntry> auditEntries)
        {
            if (auditEntries == null)
            {
                return Task.CompletedTask;
            }

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                AppAudit.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }

        private void SetNewEntityFields(ICoreEntity entity, BaseUserInfo user = null)
        {
            entity.CreatedBy = GetCurrentUserId(user);
            entity.CreatedOn = DateTime.UtcNow;
        }

        private void UpdateEntityFields(ICoreEntity entity, BaseUserInfo user = null)
        {
            entity.ModifiedBy = GetCurrentUserId(user);
            entity.ModifiedOn = DateTime.UtcNow;
        }

        private Guid GetCurrentUserId(BaseUserInfo user)
        {
            return user == null ? Guid.Empty : user.Id;
        }
    }
}
