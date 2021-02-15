using System;
using System.Collections.Generic;
using System.Text;
using Core.Common;
using Microsoft.EntityFrameworkCore;

namespace Core.Administration.Models
{
    public interface IAdministrationDbContext: IApplicationModels
    {
        DbSet<User> Users { get; set; }
        DbSet<UserAccount> UserAccounts { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Right> Rights { get; set; }
        DbSet<RoleRight> RoleRights { get; set; }
    }
}
