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
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.Business.Services.AdministrationServices
{
    public class UserService
    {
        #region Constructor

        public UserService(ICommonDataService dataBase, IObjectMapper mapper, IUserInfoService userService)
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

        #region MethodsPublic


        #region RLS

        public async Task<Dictionary<string, List<RowLevelSecurityItemViewModel>>> GetRls(Guid userId)
        {
            var user = await GetUserById(userId);

            var userRls = JsonConvert.DeserializeObject<RowLevelSecurityData>(user.Rls);

            // Parse data - set enum CrudOperation
            CrudOperationHelper.ParseCrudOperationRls(userRls);

            // Get detailed rls data on organizational structures
            var orgSecurityList =
                (await _dataBase.GetDtoAsync<OrgUnitListDto>(p => userRls.OrgUnit.Select(p => p.Id).Contains(p.Id)))
                .Select(p => new RowLevelSecurityItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    DerivedEntity = p.DerivedEntity
                }).ToList();

            // Get detailed rls user data
            var userSecurityList =
                (await _dataBase.GetDtoAsync<UserListDto>(p => userRls.User.Select(p => p.Id).Contains(p.Id)))
                .Select(p => new RowLevelSecurityItemViewModel
                {
                    Id = p.Id,
                    Name = $"{p.PersonLastName} {p.PersonFirstName} {p.PersonMiddleName}"
                });

            var atuObjectSecurityList =
                (await _dataBase.GetDtoAsync<RegionListDto>(p => userRls.AtuObject.Select(p => p.Id).Contains(p.Id)))
                .Select(p => new RowLevelSecurityItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name
                });

            // Map data
            var mapOrgUnitRls = userRls.OrgUnit.Select(p => _mapper.Map(p, orgSecurityList.SingleOrDefault(x => x.Id == p.Id)));
            var mapUserRls = userRls.User.Select(p => _mapper.Map(p, userSecurityList.SingleOrDefault(x => x.Id == p.Id)));
            var mapAtuObject = userRls.AtuObject.Select(p => _mapper.Map(p, atuObjectSecurityList.SingleOrDefault(x => x.Id == p.Id)));

            // Set to dictionary
            var dict = new Dictionary<string, List<RowLevelSecurityItemViewModel>>
            {
                {Constants.RlsType.OrgUnit, mapOrgUnitRls.ToList()},
                {Constants.RlsType.User, mapUserRls.ToList()},
                {Constants.RlsType.AtuObject, mapAtuObject.ToList()}
            };

            return dict;
        }

        public async Task CreateRls(Guid userId, RowLevelSecurityData rlsData, bool isSaveInDb)
        {
            var user = await GetUserById(userId);

            var userRls = JsonConvert.DeserializeObject<RowLevelSecurityData>(user.Rls);

            foreach (var rowLevelSecurityItem in rlsData.OrgUnit)
            {
                if (!userRls.OrgUnit.Select(p => p.Id).Contains(rowLevelSecurityItem.Id))
                {
                    userRls.OrgUnit.Add(rowLevelSecurityItem);
                }
            }

            user.Rls = JsonConvert.SerializeObject(userRls);

            _dataBase.AddDto<User>(user, true);

            if (isSaveInDb)
            {
                await _dataBase.SaveChangesAsync();
            }
            ClearUserInfoByUserId(userId);
        }

        public async Task<RowLevelSecurityData> AddRls(Guid userId, string rlsType, string crud, List<RowLevelSecurityItemViewModel> rls)
        {
            var user = await GetUserById(userId);

            var userRls = new RowLevelSecurityData();
            if (!string.IsNullOrEmpty(user.Rls))
            {
                userRls = JsonConvert.DeserializeObject<RowLevelSecurityData>(user.Rls);
                switch (rlsType)
                {
                    case Constants.RlsType.OrgUnit:
                        if (userRls.OrgUnit.Any(p => rls.Select(x => x.Id).Contains(p.Id)))
                        {
                            throw new AppException("Rls з таким Id вже існує");
                        }
                        break;
                    case Constants.RlsType.User:
                        if (userRls.User.Any(p => rls.Select(x => x.Id).Contains(p.Id)))
                        {
                            throw new AppException("Rls з таким Id вже існує");
                        }
                        break;
                    case Constants.RlsType.AtuObject:
                        if (userRls.AtuObject.Any(p => rls.Select(x => x.Id).Contains(p.Id)))
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
                        userRls.OrgUnit.Add(new RowLevelSecurityItem
                        {
                            Id = rowLevelSecurityItemViewModel.Id,
                            Els = crud
                        });
                        break;
                    case Constants.RlsType.User:
                        userRls.User.Add(new RowLevelSecurityItem
                        {
                            Id = rowLevelSecurityItemViewModel.Id,
                            Els = crud
                        });
                        break;
                    case Constants.RlsType.AtuObject:
                        userRls.AtuObject.Add(new RowLevelSecurityItem
                        {
                            Id = rowLevelSecurityItemViewModel.Id,
                            Els = crud
                        });
                        break;
                }

            }

            user.Rls = JsonConvert.SerializeObject(userRls);

            _dataBase.AddDto<User>(user, true);

            await _dataBase.SaveChangesAsync();

            ClearUserInfoByUserId(userId);

            return userRls;
        }

        public async Task<RowLevelSecurityData> EditRls(Guid userId, string rlsType, string crud, RowLevelSecurityItemViewModel rls)
        {
            var user = await GetUserById(userId);

            if (string.IsNullOrEmpty(user.Rls))
            {
                throw new AppException("Rls з таким Id не знайдено");
            }

            var userRls = JsonConvert.DeserializeObject<RowLevelSecurityData>(user.Rls);

            var userRlsEdit = rlsType switch
            {
                Constants.RlsType.OrgUnit => userRls.OrgUnit.SingleOrDefault(p => p.Id == rls.Id),
                Constants.RlsType.User => userRls.User.SingleOrDefault(p => p.Id == rls.Id),
                Constants.RlsType.AtuObject => userRls.AtuObject.SingleOrDefault(p => p.Id == rls.Id),
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

            user.Rls = JsonConvert.SerializeObject(userRls);

            _dataBase.AddDto<User>(user, true);

            await _dataBase.SaveChangesAsync();

            ClearUserInfoByUserId(userId);

            return userRls;
        }

        public async Task DeleteRls(Guid userId, string rlsType, Guid rlsId)
        {
            var user = await GetUserById(userId);

            if (string.IsNullOrEmpty(user.Rls))
            {
                throw new AppException("Rls з таким Id не знайдено");
            }

            var userRls = JsonConvert.DeserializeObject<RowLevelSecurityData>(user.Rls);

            // Find rls
            var rlsRemove = rlsType switch
            {
                Constants.RlsType.OrgUnit => userRls.OrgUnit.SingleOrDefault(p => p.Id == rlsId),
                Constants.RlsType.User => userRls.User.SingleOrDefault(p => p.Id == rlsId),
                Constants.RlsType.AtuObject => userRls.AtuObject.SingleOrDefault(p => p.Id == rlsId),
                _ => null
            };

            if (rlsRemove == null)
            {
                throw new AppException("Rls з таким Id не знайдено");
            }

            // Remove rls
            userRls.OrgUnit.Remove(rlsRemove);
            userRls.User.Remove(rlsRemove);
            userRls.AtuObject.Remove(rlsRemove);

            user.Rls = JsonConvert.SerializeObject(userRls);

            _dataBase.AddDto<User>(user, true);

            ClearUserInfoByUserId(userId);

            await _dataBase.SaveChangesAsync();
        }

        #endregion

        #region Roles

        public async Task<List<RoleDetailsDto>> GetRoles(Guid userId)
        {
            var user = await GetUserById(userId);

            if (string.IsNullOrEmpty(user.Roles))
            {
                return new List<RoleDetailsDto>();
            }

            var roles = JsonConvert.DeserializeObject<IList<Guid>>(user.Roles);

            var rolesDto = (await _dataBase.GetDtoAsync<RoleDetailsDto>(p => roles.Contains(p.Id))).ToList();

            return rolesDto;
        }

        public async Task AddRoleToUser(Guid userId, List<Guid> roleList)
        {
            var user = await GetUserById(userId);

            var userRoles = new List<Guid>();
            if (!string.IsNullOrEmpty(user.Roles))
            {
                userRoles = JsonConvert.DeserializeObject<List<Guid>>(user.Roles);

                if (userRoles.Any(roleList.Contains))
                {
                    throw new AppException("Користувач вже має цю роль");
                }
            }

            userRoles.AddRange(roleList);
            user.Roles = JsonConvert.SerializeObject(userRoles);

            _dataBase.AddDto<User>(user, true);

            await _dataBase.SaveChangesAsync();
            ClearUserInfoByUserId(userId);
        }

        public async Task DeleteRoleFromUser(Guid userId, Guid roleId)
        {
            var user = await GetUserById(userId);

            if (string.IsNullOrEmpty(user.Roles))
            {
                throw new AppException("У користувача немає ролей");
            }

            var userRoles = JsonConvert.DeserializeObject<List<Guid>>(user.Roles);

            userRoles.Remove(roleId);

            user.Roles = JsonConvert.SerializeObject(userRoles);

            _dataBase.AddDto<User>(user, true);

            await _dataBase.SaveChangesAsync();
            ClearUserInfoByUserId(userId);
        }

        #endregion

        #endregion

        #region MethodsPrivate

        private async Task<UserDetailsDto> GetUserById(Guid userId)
        {
            var user = await _dataBase.SingleOrDefaultAsync<UserDetailsDto>(p => p.Id == userId);
            if (user == null)
            {
                throw new AppException("Користувача з таким Id не знайдено");
            }

            return user;
        }

        /// <summary>
        /// Clear the cache of a user that contains a specific userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private void ClearUserInfoByUserId(Guid userId)
        {
            _userService.DeleteUserInfo(userId.ToString());
        }
        #endregion
    }
}
