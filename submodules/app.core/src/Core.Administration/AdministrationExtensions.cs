using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Administration.Helpers;
using Core.Administration.Models;
using Core.Base.Administration;
using Core.Base.Data;
using Newtonsoft.Json;

namespace Core.Administration
{
    public static class AdministrationExtensions
    {
        public static UserApplicationRights GetUserRights(this IAdministrationDbContext context, Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return new UserApplicationRights(false);
            }

            var rights = new UserApplicationRights(false)
            {
                EntityRights = GetEntityRights(context, userId),
                RowLevelRights = GetRowLevelRights(context, userId),
                Roles = GetRoles(context, userId),
                InterfaceRights = GetInterfaceRights(context, userId)
            };

            return rights;
        }

        public static async Task<UserApplicationRights> GetUserRightsAsync(this IAdministrationDbContext context, Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return new UserApplicationRights(false);
            }

            var rights = new UserApplicationRights(false)
            {
                EntityRights = await Task.Factory.StartNew(() => GetEntityRights(context, userId)),
                RowLevelRights = await Task.Factory.StartNew(() => GetRowLevelRights(context, userId)),
                Roles = await Task.Factory.StartNew(() => GetRoles(context, userId)),
                InterfaceRights = await Task.Factory.StartNew(() => GetInterfaceRights(context, userId))
            };

            return rights;
        }

        private static Dictionary<string, CrudOperation> GetEntityRights(IAdministrationDbContext context, Guid userId)
        {
            var user = context.Users.FirstOrDefault(p => p.Id == userId);
            if (user == null || string.IsNullOrEmpty(user.Roles))
            {
                return new Dictionary<string, CrudOperation>();
            }

            var roles = JsonConvert.DeserializeObject<IList<Guid>>(user.Roles);

            var rightList = new Dictionary<string, CrudOperation>();
            foreach (var userRole in roles)
            {
                var def = new[] { new { EntityName = "", AccessLevel = "" } };

                // TODO: add check isActive to role
                var rights = (from roleRight in context.RoleRights
                              join role in context.Roles on roleRight.RoleId equals userRole
                              join right in context.Rights on roleRight.RightId equals right.Id
                              where roleRight.RecordState != RecordState.Deleted && right.RecordState != RecordState.Deleted && role.RecordState != RecordState.Deleted
                              select new { right.Els}).ToList();

                var deserializeRightList = rights.Where(p=> !string.IsNullOrEmpty(p.Els)).Select(p => JsonConvert.DeserializeAnonymousType(p.Els, def));

                foreach (var els in deserializeRightList)
                {
                    foreach (var el in els)
                    {
                        if (!CrudOperationHelper.ParseCrudOperation(el.AccessLevel, out CrudOperation accessLevel))
                        {
                            continue;
                        }

                        rightList.TryAdd(el.EntityName, accessLevel);
                    }
                }
            }

            return rightList;
        }

        private static RowLevelSecurityData GetRowLevelRights(IAdministrationDbContext context, Guid userId)
        {
            var user = context.Users.FirstOrDefault(p => p.Id == userId);
            if (user == null)
            {
                return new RowLevelSecurityData();
            }

            var rlsData = new List<RowLevelSecurityData>();

            if (!string.IsNullOrEmpty(user.Rls))
            {
                var rlsUser = JsonConvert.DeserializeObject<RowLevelSecurityData>(user.Rls);

                SetRlsType(rlsUser.OrgUnit, "User");
                SetRlsType(rlsUser.User, "User");
                SetRlsType(rlsUser.AtuObject, "User");

                rlsData.Add(rlsUser);
            }

            if (!string.IsNullOrEmpty(user.Roles))
            {
                var roles = JsonConvert.DeserializeObject<IList<Guid>>(user.Roles);
                var rlsRole = context.Roles.Where(p => roles.Contains(p.Id) && p.Rls != null);

                var deserializeRlsRole = rlsRole.Select(p => JsonConvert.DeserializeObject<RowLevelSecurityData>(p.Rls)).ToList();

                foreach (var role in deserializeRlsRole)
                {
                    SetRlsType(role.OrgUnit, "Role");
                    SetRlsType(role.User, "Role");
                    SetRlsType(role.AtuObject, "Role");
                }

                rlsData.AddRange(deserializeRlsRole);
            }

            foreach (var rowLevelSecurityData in rlsData)
            {
                CrudOperationHelper.ParseCrudOperationRls(rowLevelSecurityData.OrgUnit);
                CrudOperationHelper.ParseCrudOperationRls(rowLevelSecurityData.AtuObject);
                CrudOperationHelper.ParseCrudOperationRls(rowLevelSecurityData.User);
            }

            // merge rls (user, role)
            var result = new RowLevelSecurityData();
            foreach (var rowLevelSecurityData in rlsData)
            {
                result.OrgUnit = result.OrgUnit.Concat(rowLevelSecurityData.OrgUnit).ToList();
                result.AtuObject = result.AtuObject.Concat(rowLevelSecurityData.AtuObject).ToList();
                result.User = result.User.Concat(rowLevelSecurityData.User).ToList();
            }

            return result;
        }

        /// <summary>
        /// Get all user roles
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetRoles(IAdministrationDbContext context, Guid userId)
        {
            var user = context.Users.FirstOrDefault(p => p.Id == userId);
            if (user == null || string.IsNullOrEmpty(user.Roles))
            {
                return new Dictionary<string, string>();
            }

            var roles = JsonConvert.DeserializeObject<IList<Guid>>(user.Roles);

            return context.Roles
                .Where(p => roles.Contains(p.Id))
                .Select(p => new { p.Id, p.Code })
                .ToDictionary(x => x.Id.ToString(), x => x.Code);
        }

        private static List<string> GetInterfaceRights(IAdministrationDbContext context, Guid userId)
        {
            var user = context.Users.SingleOrDefault(p => p.Id == userId);
            if (user == null || string.IsNullOrEmpty(user.Roles))
            {
                return new List<string>();
            }

            var roles = JsonConvert.DeserializeObject<IList<Guid>>(user.Roles);

            var rights = from right in context.Rights
                join roleRight in context.RoleRights on right.Id equals roleRight.RightId 
                where roles.Contains(roleRight.RoleId) && right.RightType == "Interface" && roleRight.RecordState != RecordState.Deleted select right;

            return rights.Select(p => p.Code).ToList();
        }

        /// <summary>
        /// Specify which entity the rls belongs to
        /// </summary>
        /// <param name="rlsList"></param>
        /// <param name="rlsOwner">User or Role</param>
        private static void SetRlsType(IList<RowLevelSecurityItem> rlsList, string rlsOwner)
        {
            rlsList = rlsList.Select(p =>
            {
                p.RlsOwner = rlsOwner;
                return p;
            }).ToList();
        }
    }
}
