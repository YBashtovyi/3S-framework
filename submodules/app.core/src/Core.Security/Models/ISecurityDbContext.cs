using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Core.Common;
using Core.Base.Data;

namespace Core.Security.Models
{
    public interface ISecurityDbContext: IApplicationModels
    {
        DbSet<FieldRight>? FieldRights { get; set; }
        DbSet<ProfileRole>? ProfileRoles { get; set; }
        DbSet<ProfileRight>? ProfileRights { get; set; }
        DbSet<Profile>? Profiles { get; set; }
        DbSet<Right>? Rights { get; set; }
        DbSet<RoleRight>? RoleRights { get; set; }
        DbSet<Role>? Roles { get; set; }
        DbSet<ApplicationRowLevelRight>? ApplicationRowLevelRights { get; set; }
        DbSet<RowLevelRight>? RowLevelRights { get; set; }
        DbSet<RowLevelSecurityObject>? RowLevelSecurityObjects { get; set; }
        DbSet<UserProfile>? UserProfiles { get; set; }
        DbSet<UserDefaultValue>? UserDefaultValues { get; set; }
        DbSet<OperationRight>? OperationRights { get; set; }
        DbSet<ProfileOperationRight>? ProfileOperationRights { get; set; }
        DbSet<RoleOperationRight>? RoleOperationRights { get; set; }
        DbSet<SimpleDataDto>? SimpleDataDtos { get; set; }

        IEnumerable<string> GetIdentifiersRecursive(string entityName, string parentColumnName, IEnumerable<string> parentIds);
    }
}
