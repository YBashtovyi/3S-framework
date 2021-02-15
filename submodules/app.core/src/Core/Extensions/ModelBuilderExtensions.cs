using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void UseSnakeCaseNaming(this ModelBuilder builder)
        {
            var entityTypes = builder.Model.GetEntityTypes();
            // from Pascal case to snake case
            foreach (var entity in entityTypes)
            {
                // Replace table names
                // It is possible only if it is entity with primary key and not derived from the type that is already in context
                if (!entity.IsKeyless && !entityTypes.Any(mentity => mentity.ClrType == entity.ClrType.BaseType))
                {
                    entity.SetTableName(entity.GetTableName().ToSnakeCase());
                }

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetName(index.GetName().ToSnakeCase());
                }
            }
        }
    }
}
