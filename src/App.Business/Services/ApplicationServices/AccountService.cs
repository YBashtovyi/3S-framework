using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.Administration;
using App.Data.Dto.Common;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Data;
using Core.Services;
using Core.Services.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace App.Business.Services.ApplicationServices
{
    public class AccountService
    {
        #region Constructors

        public AccountService(ICommonDataService dataService,
            IUserInfoService userService,
            ILogger<AccountService> logger,
            IStringLocalizer<AccountService> localizer, IHttpContextAccessor httpContextAccessor)
        {
            DataService = dataService;
            _userService = userService;
            _logger = logger;
            Localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Properties

        public ICommonDataService DataService { get; }
        public IStringLocalizer<AccountService> Localizer { get; }
        public BaseUserInfo CurrentUser => _userService?.GetCurrentUserInfo();

        #endregion

        #region Fields 

        private readonly IUserInfoService _userService;

        private readonly ILogger<AccountService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region MethodsPublic

        

        /// <summary>
        /// Gets user login profiles for current user id (identity id)
        /// </summary>
        /// <remarks>Serves only for controller so it throws exceptions with messages appropriate to authentication</remarks>
        /// <returns>Profiles list to select</returns>
        /// <exception cref="AppException">If user is not logged in on identity.
        /// Or application person for identity id does not exist.
        /// Or employee is not created for person.
        /// Or security profile is not set for employee</exception>
        public async Task<UserLoginDto> GetUserDataAsync()
        {
            var currentUser = CurrentUser;

            if (currentUser.Id == Guid.Empty)
            {
                throw new AppException(Localizer["User is not authenticated on Identity server"], Localizer["Unauthorized"]);
            }

            // TODO: Переписать комменты, и предвидеть возможно, переделать выборку по организациям
            var user = await DataService.SingleOrDefaultAsync<UserLoginDto>(p => p.Id == currentUser.Id);
            if (user == null)
            {
                throw new AppException("Користувач не знайдений", Localizer["Unauthorized"]);
            }

            return user;
        }

        

        public async Task<UserFullDto> GetUserFullDtoAsync()
        {
            var user = await _userService.GetCurrentUserInfoAsync();

            if (string.IsNullOrEmpty(user.AccountId))
            {
                _logger.LogWarning("Authentication error. User is not authenticated on Identity server. User id in not set");
                return null;
                throw new ApplicationException("User is not authenticated on Identity server. User id in not set");
            }

            if (user.Id == Guid.Empty)
            {
                _logger.LogWarning("Security profile is not set to current user, try relogin. User id at identity server: {IdentityUserId}", user.AccountId);
                return new UserFullDto();
                throw new ApplicationException($"Security profile is not set to current user, try relogin. User id at identity server:{user.Id}");
            }

            var data = await DataService.FirstOrDefaultAsync<UserFullDto>(x =>
                x.UserId == user.Id,
                _userService.SystemUser);

            if (user is UserInfo userInfo)
            {
                data.Rights = userInfo.Rights;
            }

            return data;
        }


        #endregion

        #region MethodsPrivate

        #endregion
    }
}
