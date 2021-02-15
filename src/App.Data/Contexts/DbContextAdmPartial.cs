using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using App.Data.Dto.Administration;
using Core.Administration.Models;
using Core.Base.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Contexts
{
    public partial class AppDbContext: IAdministrationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<RoleRight> RoleRights { get; set; }

        public DbSet<UserAccountDto> UserAccountDtos { get; set; }
        public DbSet<UserLoginDto> UserProfileLoginDtos { get; set; }
        public DbSet<UserFullDto> UserFullDtos { get; set; }
        public DbSet<UserListDto> UserListDtos { get; set; }
        public DbSet<UserDetailsDto> UserDetailsDtos { get; set; }

        public DbSet<RoleListDto> RoleListDtos { get; set; }
        public DbSet<RoleDetailsDto> RoleDetailsDtos { get; set; }
        public DbSet<RoleEditDto> RoleEditDtos { get; set; }
        public DbSet<RoleRightListDto> RoleRightListDtos { get; set; }
        public DbSet<UserRoleListDto> UserRoleListDtos { get; set; }

        public DbSet<RightListDto> RightListDtos { get; set; }
        public DbSet<RightDetailsDto> RightDetailsDtos { get; set; }
        public DbSet<RightEditDto> RightEditDtos { get; set; }

        private static readonly ConcurrentDictionary<string, HierarchyTableData> _hierarchyTables = new ConcurrentDictionary<string, HierarchyTableData>();

        private void BuildAdmModels(ModelBuilder builder)
        {
            builder.Entity<UserAccountDto>().HasNoKey();
            builder.Entity<UserLoginDto>().HasNoKey();
            builder.Entity<UserFullDto>().HasNoKey();
            builder.Entity<UserListDto>().HasNoKey();
            builder.Entity<UserDetailsDto>().HasNoKey();

            builder.Entity<RoleListDto>().HasNoKey();
            builder.Entity<RoleDetailsDto>().HasNoKey();
            builder.Entity<RoleEditDto>().HasNoKey();
            builder.Entity<RoleRightListDto>().HasNoKey();
            builder.Entity<UserRoleListDto>().HasNoKey();

            builder.Entity<RightListDto>().HasNoKey();
            builder.Entity<RightDetailsDto>().HasNoKey();
            builder.Entity<RightEditDto>().HasNoKey();
        }

        private void IgnoreKeylessTypesWhenCreateTables(ModelBuilder builder)
        {
            var entityTypes = builder.Model.GetEntityTypes();
            // from Pascal case to snake case
            foreach (var entity in entityTypes)
            {
                if (entity.IsKeyless)
                {
                    builder.Entity(entity.ClrType).ToView(null);
                }
            }
        }

        public IEnumerable<string> GetIdentifiersRecursive(string entityName, string parentColumnId, IEnumerable<string> parentIds)
        {
            if (parentIds == null)
            {
                throw new ArgumentNullException("Argument ids cannot be null");
            }

            if (!_hierarchyTables.TryGetValue(entityName, out var table))
            {
                var entityType = Model.GetEntityTypes().FirstOrDefault(x => x.ClrType.Name == entityName);
                if (entityType == null)
                {
                    throw new ArgumentException($"Cannot find entity by name {entityName}");
                }
                var property = entityType.FindProperty(parentColumnId);
                if (property == null)
                {
                    throw new ArgumentException($"Cannot find property {parentColumnId} in entity {entityName}");
                }

                table = new HierarchyTableData { Name = entityType.GetTableName(), ParentColumnName = property.GetColumnName() };
                _hierarchyTables.TryAdd(entityName, table);
            }

            // check for empty guid because ids are guids in database
            // and we need only children with non-empty parent id
            var emptyStringGuid = Guid.Empty.ToString();
            var filteredIds = parentIds.Where(x => !string.IsNullOrEmpty(x) && x != emptyStringGuid).ToArray();

            if (filteredIds == null)
            {
                throw new ArgumentException("ids should contain at least one non-default value");
            }

            var placeHoldersList = new List<string>(filteredIds.Length);
            for (var i = 0; i < filteredIds.Length; i++)
            {
                var valuePlaceholder = CreateValuePlaceholderWithCast(i);
                placeHoldersList.Add(valuePlaceholder);
            }
            var placeHolders = string.Join(",", placeHoldersList);
            var conditionString = $"({table.ParentColumnName} in ({placeHolders}))";

            var sqlConditions = FormattableStringFactory.Create(conditionString, filteredIds);

            //TODO : try to use StringBuilder 
            var queryText = @$"WITH RECURSIVE r AS (
   SELECT id, {table.ParentColumnName}
   FROM {table.Name} 
   WHERE {conditionString}

   UNION ALL

   SELECT tab.id, tab.parent_id
   FROM {table.Name} as tab
      JOIN r
          ON tab.parent_id = r.id and tab.id <> r.id
)

SELECT cast(r.id as text) as data FROM r;";

            var finalQuery = FormattableStringFactory.Create(queryText, sqlConditions.GetArguments());
            var data = Set<SimpleDataDto>().FromSqlInterpolated(finalQuery).Select(x => x.Data);
            return data.ToArray().Distinct().ToArray(); // ToArray() before distinct prevents strange EF exception
        }

        // TODO: THIS WON'T WORK ON MSSQL or other Databases where there is no UUID type
        private string CreateValuePlaceholderWithCast(int parameterIndex)
        {
            var castTo = "uuid";
            return "cast({" + parameterIndex + "} as " + castTo + ")";
        }

        private class HierarchyTableData
        {
            public string Name { get; set; }
            public string ParentColumnName { get; set; }
        }
    }
}
