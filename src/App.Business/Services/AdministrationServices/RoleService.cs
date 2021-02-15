using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Dto.Administration;
using App.Data.Dto.Administration.NotMapped;
using App.Data.Dto.Atu;
using App.Data.Dto.Org;
using Core.Administration.Helpers;
using Core.Administration.Models;
using Core.Base.Administration;
using Core.Base.Exceptions;
using Core.Services;
using Core.Services.Data;
using Newtonsoft.Json;

namespace App.Business.Services.AdministrationServices
{
    public class RoleService
    {
        #region Constructor

        public RoleService(ICommonDataService dataBase, IObjectMapper mapper, IUserInfoService userService)
        {
            _dataBase = dataBase;
            _mapper = mapper;
            _userService = userService;
        }

        #endregion

        #region Properties

        private readonly ICommonDataService _dataBase;
        private readonly IObjectMapper _mapper;
        private readonly IUserInfoService _userService;

        #endregion

        public async Task<List<RoleRightListDto>> GetRoleRight(Guid roleId)
        {
            var role = await GetRoleById(roleId);

            var rights = await _dataBase.GetDtoAsync<RoleRightListDto>(p => p.RoleId == role.Id);

            return rights.ToList();
        }

        public async Task CreateRoleRight(Guid roleId, List<Guid> rightIds)
        {
            var role = await GetRoleById(roleId);

            var rights = (await _dataBase.GetDtoAsync<RightDetailsDto>(p => rightIds.Contains(p.Id))).ToList();
            if (!rights.Any())
            {
                throw new AppException("Право з таким Id не знайдено");
            }

            foreach (var rightDetailsDto in rights)
            {
                var roleRight = new RoleRight
                {
                    Id = Guid.NewGuid(),
                    RightId = rightDetailsDto.Id,
                    RoleId = role.Id
                };

                _dataBase.Add(roleRight, false);
            }
            

            await _dataBase.SaveChangesAsync();
            await ClearUserInfoByRoleId(roleId);
        }

        public async Task DeleteRoleRight(Guid roleRightId)
        {
            var roleRight = await _dataBase.SingleOrDefaultAsync<RoleRightListDto>(p => p.Id == roleRightId);
            if (roleRight == null)
            {
                throw new AppException("Право ролі не знайдено");
            }

            _dataBase.Remove<RoleRight>(roleRight.Id, false);

            await _dataBase.SaveChangesAsync();
            await ClearUserInfoByRoleId(roleRight.RoleId);
        }

        #region RLS

        public async Task<Dictionary<string, List<RowLevelSecurityItemViewModel>>> GetRls(Guid userId)
        {
            var role = await GetRoleById(userId);

            var roleRls = JsonConvert.DeserializeObject<RowLevelSecurityData>(role.Rls);

            // Parse data - set enum CrudOperation
            CrudOperationHelper.ParseCrudOperationRls(roleRls);

            // Get detailed rls data on organizational structures
            var orgSecurityList =
                (await _dataBase.GetDtoAsync<OrgUnitListDto>(p => roleRls.OrgUnit.Select(p => p.Id).Contains(p.Id)))
                .Select(p => new RowLevelSecurityItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    DerivedEntity = p.DerivedEntity
                }).ToList();

            // Get detailed rls user data
            var userSecurityList =
                (await _dataBase.GetDtoAsync<UserListDto>(p => roleRls.User.Select(p => p.Id).Contains(p.Id)))
                .Select(p => new RowLevelSecurityItemViewModel
                {
                    Id = p.Id,
                    Name = $"{p.PersonLastName} {p.PersonFirstName} {p.PersonMiddleName}"
                });

            var atuObjectSecurityList =
                (await _dataBase.GetDtoAsync<RegionListDto>(p => roleRls.AtuObject.Select(p => p.Id).Contains(p.Id)))
                .Select(p => new RowLevelSecurityItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name
                });

            // Map data
            var mapOrgUnitRls = roleRls.OrgUnit.Select(p => _mapper.Map(p, orgSecurityList.SingleOrDefault(x => x.Id == p.Id)));
            var mapUserRls = roleRls.User.Select(p => _mapper.Map(p, userSecurityList.SingleOrDefault(x => x.Id == p.Id)));
            var mapAtuObject = roleRls.AtuObject.Select(p => _mapper.Map(p, atuObjectSecurityList.SingleOrDefault(x => x.Id == p.Id)));

            // Set to dictionary
            var dict = new Dictionary<string, List<RowLevelSecurityItemViewModel>>
            {
                {Constants.RlsType.OrgUnit, mapOrgUnitRls.ToList()},
                {Constants.RlsType.User, mapUserRls.ToList()},
                {Constants.RlsType.AtuObject, mapAtuObject.ToList()}
            };

            return dict;
        }

        public async Task CreateRls(Guid roleId, RowLevelSecurityData rlsData, bool isSaveInDb)
        {
            var role = await GetRoleById(roleId);

            var roleRls = JsonConvert.DeserializeObject<RowLevelSecurityData>(role.Rls);

            foreach (var rowLevelSecurityItem in rlsData.OrgUnit)
            {
                if (!roleRls.OrgUnit.Select(p => p.Id).Contains(rowLevelSecurityItem.Id))
                {
                    roleRls.OrgUnit.Add(rowLevelSecurityItem);
                }
            }

            role.Rls = JsonConvert.SerializeObject(roleRls);

            _dataBase.AddDto<Role>(role, true);

            if (isSaveInDb)
            {
                await _dataBase.SaveChangesAsync();
            }

            await ClearUserInfoByRoleId(roleId);
        }

        public async Task<RowLevelSecurityData> AddRls(Guid roleId, string rlsType, string crud, List<RowLevelSecurityItemViewModel> rls)
        {
            var role = await GetRoleById(roleId);

            var roleRls = new RowLevelSecurityData();
            if (!string.IsNullOrEmpty(role.Rls))
            {
                roleRls = JsonConvert.DeserializeObject<RowLevelSecurityData>(role.Rls);
                switch (rlsType)
                {
                    case Constants.RlsType.OrgUnit:
                        if (roleRls.OrgUnit.Any(p => rls.Select(x => x.Id).Contains(p.Id)))
                        {
                            throw new AppException("Rls з таким Id вже існує");
                        }
                        break;
                    case Constants.RlsType.User:
                        if (roleRls.User.Any(p => rls.Select(x => x.Id).Contains(p.Id)))
                        {
                            throw new AppException("Rls з таким Id вже існує");
                        }
                        break;
                    case Constants.RlsType.AtuObject:
                        if (roleRls.AtuObject.Any(p => rls.Select(x => x.Id).Contains(p.Id)))
                        {
                            throw new AppException("Rls з таким Id вже існує");
                        }
                        break;
                }

            }

            foreach (var rowLevelSecurityItemViewModel in rls)
            {
                switch (rlsType)
                {
                    case Constants.RlsType.OrgUnit:
                        roleRls.OrgUnit.Add(new RowLevelSecurityItem
                        {
                            Id = rowLevelSecurityItemViewModel.Id,
                            Els = crud
                        });
                        break;
                    case Constants.RlsType.User:
                        roleRls.User.Add(new RowLevelSecurityItem
                        {
                            Id = rowLevelSecurityItemViewModel.Id,
                            Els = crud
                        });
                        break;
                    case Constants.RlsType.AtuObject:
                        roleRls.AtuObject.Add(new RowLevelSecurityItem
                        {
                            Id = rowLevelSecurityItemViewModel.Id,
                            Els = crud
                        });
                        break;
                }

            }

            role.Rls = JsonConvert.SerializeObject(roleRls);

            _dataBase.AddDto<Role>(role, true);

            await _dataBase.SaveChangesAsync();

            await ClearUserInfoByRoleId(roleId);

            return roleRls;
        }

        public async Task<RowLevelSecurityData> EditRls(Guid roleId, string rlsType, string crud, RowLevelSecurityItemViewModel rls)
        {
            var role = await GetRoleById(roleId);

            if (string.IsNullOrEmpty(role.Rls))
            {
                throw new AppException("Rls з таким Id не знайдено");
            }

            var roleRls = JsonConvert.DeserializeObject<RowLevelSecurityData>(role.Rls);

            var userRlsEdit = rlsType switch
            {
                Constants.RlsType.OrgUnit => roleRls.OrgUnit.SingleOrDefault(p => p.Id == rls.Id),
                Constants.RlsType.User => roleRls.User.SingleOrDefault(p => p.Id == rls.Id),
                Constants.RlsType.AtuObject => roleRls.AtuObject.SingleOrDefault(p => p.Id == rls.Id),
                _ => null
            };

            if (userRlsEdit == null)
            {
                throw new AppException("Rls з таким Id не знайдено");
            }

            if (string.IsNullOrEmpty(crud))
            {
                crud = "N";
            }

            userRlsEdit.Els = crud;

            role.Rls = JsonConvert.SerializeObject(roleRls);

            _dataBase.AddDto<Role>(role, true);

            await _dataBase.SaveChangesAsync();

            await ClearUserInfoByRoleId(roleId);

            return roleRls;
        }

        public async Task DeleteRls(Guid roleId, string rlsType, Guid rlsId)
        {
            var role = await GetRoleById(roleId);

            if (string.IsNullOrEmpty(role.Rls))
            {
                throw new AppException("Rls з таким Id не знайдено");
            }

            var roleRls = JsonConvert.DeserializeObject<RowLevelSecurityData>(role.Rls);

            // Find rls
            var rlsRemove = rlsType switch
            {
                Constants.RlsType.OrgUnit => roleRls.OrgUnit.SingleOrDefault(p => p.Id == rlsId),
                Constants.RlsType.User => roleRls.User.SingleOrDefault(p => p.Id == rlsId),
                Constants.RlsType.AtuObject => roleRls.AtuObject.SingleOrDefault(p => p.Id == rlsId),
                _ => null
            };

            if (rlsRemove == null)
            {
                throw new AppException("Rls з таким Id не знайдено");
            }

            // Remove rls
            roleRls.OrgUnit.Remove(rlsRemove);
            roleRls.User.Remove(rlsRemove);
            roleRls.AtuObject.Remove(rlsRemove);

            role.Rls = JsonConvert.SerializeObject(roleRls);

            _dataBase.AddDto<Role>(role, true);

            await _dataBase.SaveChangesAsync();

            await ClearUserInfoByRoleId(roleId);
        }

        #endregion

        #region MethodsPrivate

        private async Task<RoleDetailsDto> GetRoleById(Guid roleId)
        {
            var role = await _dataBase.SingleOrDefaultAsync<RoleDetailsDto>(p => p.Id == roleId);
            if (role == null)
            {
                throw new AppException("Роль з таким Id не знайдено");
            }

            return role;
        }

        /// <summary>
        /// Clear the cache of a user that contains a specific role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        private async Task ClearUserInfoByRoleId(Guid roleId)
        {
            var users = await _dataBase.GetDtoAsync<UserRoleListDto>(p => p.RoleId == roleId);
            foreach (var user in users)
            {
                _userService.DeleteUserInfo(user.UserId.ToString());
            }
        }

        #endregion
    }
}
