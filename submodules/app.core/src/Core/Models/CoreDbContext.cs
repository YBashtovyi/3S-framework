using System;
using System.Collections.Generic;
using Core.Common.Extensions;
using Core.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Models
{
    public class CoreDbContext: DbContext, IApplicationModels, IDbContext
    {
        /// <summary>
        /// Classes used in db set, where:
        /// key - type
        /// value - entity has key
        /// </summary>
        private static Dictionary<Type, bool> ApplicationModels;

        public CoreDbContext()
        {
                
        }

        public CoreDbContext(DbContextOptions options): base(options)
        {
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        public virtual IEnumerable<Type> GetApplicationModels(bool withPrimaryKeysOnly)
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
                            models.Add(dbSetClass, false);
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
    }
}
