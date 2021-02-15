using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Core.Administration;
using Core.Administration.Models;
using Core.Base.Exceptions;
using Core.Data;
using Core.Security;
using Core.Security.Models;
using Core.Services.Data;
using Core.Services.DistributedCacheService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
    public class UserInfoService: IUserInfoService
    {
        public BaseUserInfo SystemUser => _systemUser;

        protected readonly IDistributedCacheService CacheService;
        protected readonly IHttpContextAccessor HttpContextAccessor;
        protected readonly IAdministrationDbContext DbContext;
        protected readonly IConfiguration Configuration;
        protected readonly string CacheSuffix = "_userInfo";
        protected readonly string CacheRightsSuffix = "_userRights";
        private static readonly UserInfo _systemUser;
        private UserInfo UserInfo;

        static UserInfoService()
        {
            _systemUser = new UserInfo
            {
                Rights = new UserApplicationRights(true)
                {
                    //EntityRights = new Dictionary<string, EntityRightData>(),
                    //OperationRights = new Dictionary<string, AccessLevel>(),
                    //RowLevelRights = new Dictionary<string, List<RowLevelRightRule>>()
                }
            };
        }

        public UserInfoService(IAdministrationDbContext dbContext, IDistributedCacheService cacheService, IHttpContextAccessor httpContextAccessor, IConfiguration confguration)
        {
            CacheService = cacheService;
            HttpContextAccessor = httpContextAccessor;
            DbContext = dbContext;
            Configuration = confguration;
        }

        public BaseUserInfo GetUserInfo(string userId)
        {
            var userInfo = GetUserInfoInternal(userId);
            if (userInfo == null)
            {
                return new UserInfo();
            }

            return Clone(userInfo);
        }

        public virtual async Task<BaseUserInfo> GetUserInfoAsync(string userId)
        {
            var userInfo = await GetUserInfoInternalAsync(userId);
            if (userInfo == null)
            {
                return new UserInfo();
            }

            return Clone(userInfo);
        }

        public virtual BaseUserInfo GetCurrentUserInfo()
        {
            var (accountId, loginData) = GetCurrentLoginData();
            if (string.IsNullOrEmpty(accountId))
            {
                return new UserInfo();
            }

            var userAccount = DbContext.UserAccounts.SingleOrDefault(p => p.AccountId == accountId);
            if (userAccount == null)
            {
                return new UserInfo();
            }

            var userInfo = GetUserInfoInternal(userAccount.UserId.ToString(), true);
            
            if (userInfo.Id != Guid.Empty) return Clone(userInfo);

            userInfo = new UserInfo
            {
                Id = userAccount.UserId,
                AccountId = accountId,
                LoginData = loginData,
                CultureInfo = new BaseUserCultureInfo()
            };

            SaveUserInfoToCache(userInfo);
            return userInfo;

        }

        public virtual async Task<BaseUserInfo> GetCurrentUserInfoAsync()
        {
            var (accountId, loginData) = GetCurrentLoginData();
            if (string.IsNullOrEmpty(accountId))
            {
                return new UserInfo();
            }

            var userAccount = DbContext.UserAccounts.SingleOrDefault(p => p.AccountId == accountId);
            if (userAccount == null)
            {
                return new UserInfo();
            }

            var userInfo = await GetUserInfoInternalAsync(userAccount.UserId.ToString(), true);

            if (userInfo.LoginData != null)
            {
                loginData.TryGetValue("AuthProvider", out var currentAuthProvider);
                userInfo.LoginData.TryGetValue("AuthProvider", out var cacheAuthProvider);

                if (currentAuthProvider != cacheAuthProvider)
                {
                    return CreateUserInfoAndSaveToCache(userAccount.UserId, accountId, loginData);
                }
            }

            if (userInfo.Id != Guid.Empty) return Clone(userInfo);
            
            userInfo = new UserInfo
            {
                Id = userAccount.UserId,
                AccountId = accountId,
                LoginData = loginData,
                CultureInfo = new BaseUserCultureInfo()
            };

            SaveUserInfoToCache(userInfo);
            return userInfo;

        }

        public virtual bool UpdateUserInfo(Guid userId, string accountId)
        {
            var userInfo = GetUserInfoInternal(userId.ToString());
            userInfo.Rights = null;
            // TODO: dont use (personFullName, organzationName)
            return UpdateUserInfo(userInfo, userId, accountId, string.Empty, string.Empty, false);
        }

        public virtual bool UpdateCurrentUserInfo(Guid userId, string accountId, string personFullName, string organizationName)
        {
            var userInfo = GetCurrentUserInfo() as UserInfo;
            userInfo.Rights = null;
            return UpdateUserInfo(userInfo, userId, accountId, personFullName, organizationName, true);
        }

        public virtual void DeleteCurrentUserInfo()
        {
            var (accountId, _) = GetCurrentLoginData();

            var userAccount = DbContext.UserAccounts.SingleOrDefault(p => p.AccountId == accountId);
            if (userAccount == null)
            {
                throw new AppException("Користувач не знайдений.");
            }

            DeleteUserInfo(userAccount.UserId.ToString());
            UserInfo = null;
        }

        public virtual void DeleteUserInfo(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return;
            
            var rightsCacheKey = userId + CacheRightsSuffix;
            CacheService.ClearKey(rightsCacheKey);

            var cacheKey = userId + CacheSuffix;
            CacheService.ClearKey(cacheKey);
        }

        private bool UpdateUserInfo(UserInfo userInfo, Guid userId, string accountId, string personFullName, string organizationName, bool isCurrent)
        {
            if (userInfo?.Id == null)
            {
                return false;
            }

            if (userInfo.Id != userId )
            {
                userInfo.Id = userId;
                userInfo.AccountId = accountId;
                userInfo.LoginData = new Dictionary<string, string>
                {
                    {"personFullName", personFullName},
                    {"organizationName", organizationName}
                };

                if (isCurrent)
                {
                    UserInfo.AccountId = accountId;
                    // we need update rights now so the current user could use it within current scope
                    UpdateUserRights(userInfo);
                    UserInfo.Rights = userInfo.Rights;
                    UserInfo.LoginData = new Dictionary<string, string>
                    {
                        {"personFullName", personFullName},
                        {"organizationName", organizationName}
                    };
                }

                SaveUserInfoToCache(userInfo);
            }

            return true;
        }

        protected virtual UserInfo GetUserInfoInternal(string userId, bool isCurrentUser = false)
        {
            var userInfo = GetUserInfoFromCache(userId, isCurrentUser);

            if (UpdateUserRights(userInfo))
            {
                SaveUserInfoToCache(userInfo);
            }

            return userInfo;
        }

        protected virtual async Task<UserInfo> GetUserInfoInternalAsync(string userId, bool isCurrentUser = false)
        {
            var userInfo = GetUserInfoFromCache(userId, isCurrentUser);

            if (await UpdateUserRightsAsync(userInfo))
            {
                SaveUserInfoToCache(userInfo);
            }

            return userInfo;
        }

        protected virtual bool SaveUserInfoToCache(UserInfo userInfo)
        {
            if (userInfo.Id == Guid.Empty)
            {
                return false;
            }

            var rightsCacheDuration = Configuration.GetValue<int?>("Caching:Rights");
            TimeSpan? expiry = null;
            if (rightsCacheDuration.HasValue)
            {
                expiry = TimeSpan.FromSeconds(rightsCacheDuration.Value);
            }

            var rightsCacheKey = userInfo.Id + CacheRightsSuffix;
            CacheService.SetValue(rightsCacheKey, userInfo.Rights, expiry);

            var userInfoCacheDuration = Configuration.GetValue<int?>("Caching:UserInfo");
            expiry = null;
            if (userInfoCacheDuration.HasValue)
            {
                expiry = TimeSpan.FromSeconds(userInfoCacheDuration.Value);
            }
            var cacheKey = userInfo.Id + CacheSuffix;
            // should save user without rights, because rights stored separately with own invalidation time
            var clone = Clone(userInfo);
            clone.Rights = null;
            CacheService.SetValue(cacheKey, clone, expiry);

            return true;
        }

        protected virtual (string accountId, Dictionary<string, string> loginData) GetCurrentLoginData()
        {
            var httpContext = HttpContextAccessor?.HttpContext;
            var userLogin = httpContext?.User?.FindFirst("name")?.Value;
            var id = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var authProvider = httpContext?.User.FindFirst("authProvider")?.Value;
            if (!string.IsNullOrEmpty(authProvider))
            {
                id = httpContext?.User.FindFirst("drfocode")?.Value;
            }

            var loginData = new Dictionary<string, string>
            {
                ["UserLogin"] = userLogin,
                ["AuthProvider"] = authProvider
            };

            return (id, loginData);
        }

        protected virtual UserInfo GetUserInfoFromCache(string userId, bool isCurrentUser)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new UserInfo();
            }

            UserInfo userInfo = null;
            if (isCurrentUser)
            {
                userInfo = UserInfo;
            }

            if (userInfo == null)
            {
                userInfo = CacheService.GetValue<UserInfo>(userId + CacheSuffix);
                if (isCurrentUser)
                {
                    UserInfo = userInfo;
                }
            }

            if (userInfo == null)
            {
                return new UserInfo();
            }

            // rights are stored separately from user
            if (userInfo?.Rights == null)
            {
                userInfo.Rights = CacheService.GetValue<UserApplicationRights>(userId + CacheRightsSuffix);
            }

            return userInfo;
        }

        private bool UpdateUserRights(UserInfo userInfo)
        {
            if (userInfo == null || userInfo.Rights != null)
            {
                return false;
            }

            userInfo.Rights = DbContext?.GetUserRights(userInfo.Id);

            return true;
        }

        private async Task<bool> UpdateUserRightsAsync(UserInfo userInfo)
        {
            if (userInfo == null || userInfo.Rights != null)
            {
                return false;
            }

            userInfo.Rights = await DbContext?.GetUserRightsAsync(userInfo.Id);

            return true;
        }

        private UserInfo Clone(UserInfo userInfo)
        {
            var clone = new UserInfo
            {
                Id = userInfo.Id,
                LoginData = userInfo.LoginData,
                AccountId = userInfo.AccountId,
                CultureInfo = userInfo.CultureInfo,
                Rights = userInfo.Rights
            };

            return clone;
        }

        private UserInfo CreateUserInfoAndSaveToCache(Guid userId, string accountId, Dictionary<string, string> loginData)
        {
            var userInfo = new UserInfo
            {
                Id = userId,
                AccountId = accountId,
                LoginData = loginData,
                CultureInfo = new BaseUserCultureInfo()
            };

            SaveUserInfoToCache(userInfo);
            return userInfo;
        }
    }
}
