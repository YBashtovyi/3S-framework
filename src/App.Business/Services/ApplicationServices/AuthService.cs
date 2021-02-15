using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using App.Data.Dto.Administration;
using Core.Administration;
using Core.Base.Exceptions;
using Core.Data;
using Core.Services;
using Core.Services.Data;
using Core.Services.DistributedCacheService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace App.Business.Services.ApplicationServices
{
    public class AuthService
    {
        public BaseUserInfo CurrentUser => _userService?.GetCurrentUserInfo();

        private readonly IDistributedCacheService _cacheService;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        private readonly ICommonDataService _dataService;
        private readonly IUserInfoService _userService;

        protected readonly IHttpContextAccessor  HttpContextAccessor;
        private readonly ILogger<AuthService> _logger;
        private readonly IStringLocalizer<AuthService> _localizer;

        public AuthService(IConfiguration configuration, TokenService tokenService, ICommonDataService dataService, IUserInfoService userService, IHttpContextAccessor httpContextAccessor, ILogger<AuthService> logger, IDistributedCacheService cacheService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _dataService = dataService;
            _userService = userService;
            HttpContextAccessor = httpContextAccessor;
            _logger = logger;
            _cacheService = cacheService;
        }

        #region IdGovUa

        

        #endregion

        public string GetAuthIdGovUaUrl()
        {
            var redirectUri = _configuration["IdGovUaProvider:RedirectUri"];
            var clientId = _configuration["IdGovUaProvider:ClientId"];
            var authUri = _configuration["IdGovUaProvider:Url"];

            var state = Guid.NewGuid().ToString();
            _cacheService.SetValue($"auth_state_{state}", 1);

            return $"{authUri}/?response_type=code&client_id={clientId}&state={state}&redirect_uri={redirectUri}";
        }

        public async Task<object> AuthenticateIdGovUa(string state, string code)
        {
            if (string.IsNullOrEmpty(state))
            {
                throw new AppException("Сервіс \"Інтегрована система електронної ідентифікації\" https://id.gov.ua/ не надав state для авторизації.");
            }

            var cacheState = _cacheService.GetValue<int>($"auth_state_{state}");
            if (cacheState != 1)
            {
                throw new AppException("Помилка, не вірне значення параметру state");
            }

            _cacheService.ClearKey($"auth_state_{state}");

            if (string.IsNullOrEmpty(code))
            {
                throw new AppException("Сервіс \"Інтегрована система електронної ідентифікації\" https://id.gov.ua/ не надав код для авторизації.");
            }

            var tokenModel = await _tokenService.GetIdGovUaToken(code);
            if (tokenModel == null)
            {
                throw new AppException("Сервіс \"Інтегрована система електронної ідентифікації\" https://id.gov.ua/ " +
                                       "не відповідає. Доступ на веб - портал TEST не може бути наданий. Будь - ласка спробуйте пізніше.");
            }

            var userInfo = await _tokenService.GetIdGovUaUserInfo(tokenModel);
            if (userInfo == null)
            {
                throw new AppException("Сервіс \"Інтегрована система електронної ідентифікації\" https://id.gov.ua/ не" +
                                       " підтвердив Вашу особу. Доступ на веб - портал ТЕСТ не надано.");
            }

            if (!string.IsNullOrEmpty(userInfo.edrpoucode))
            {
                if (userInfo.edrpoucode.All(char.IsDigit))
                {
                    if (userInfo.edrpoucode.Length != 8)
                    {
                        userInfo.drfocode = userInfo.edrpoucode;
                        userInfo.edrpoucode = string.Empty;
                    }
                }
                else
                {
                    userInfo.drfocode = userInfo.edrpoucode;
                    userInfo.edrpoucode = string.Empty;
                }
            }

            if (string.IsNullOrEmpty(userInfo.drfocode))
            {
                throw new AppException("Згідно інформації, що надійшла від \"Інтегрованой системи електронної ідентифікації\" https://id.gov.ua/ у Вашій ЕЦП не заповнене поле \"Код РНОКПП (Індивідуальний податковий номер) користувача\"." +
                                       "В зв'язку з цим, ми не можемо ідентифікувати Вашу особу.");
            }

            if (userInfo.edrpoucode == userInfo.drfocode)
                userInfo.edrpoucode = "";

            var userProfileData = await _dataService.SingleOrDefaultAsync<UserAccountDto>(p =>
                p.AuthProvider == "IdGovUa" && p.AccountId == userInfo.drfocode, _userService.SystemUser);
            if (userProfileData == null)
            {
                throw new AppException("Користувач з IdGovUa не прив'язаний до Системи управління інфраструктурними будівельними проєктами." +
                                       " Будь ласка, зверніться до адміністратора");
            }

            var claims = new List<Claim>
            {
                new Claim("fullName", userInfo.subjectcn, ClaimValueTypes.String),
                new Claim("lastname", userInfo.lastname, ClaimValueTypes.String),
                new Claim("drfocode", userInfo.drfocode, ClaimValueTypes.String),
                new Claim("authProvider", "IdGovUa")
            };

            var userIdentity = new ClaimsIdentity(claims, "SecureLogin");
            var userPrincipal = new ClaimsPrincipal(userIdentity);


            await HttpContextAccessor.HttpContext.SignInAsync("Cookies",
                userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddHours(8),
                    IsPersistent = false,
                    AllowRefresh = false
                });

            HttpContextAccessor.HttpContext.User = userPrincipal;

            // TODO: Работу с выборкой организации переделать. Так как юзер может быть в нескольких, как сейвить пока не ясно
            _userService.UpdateCurrentUserInfo(userProfileData.UserId, userProfileData.AccountId, string.Empty, string.Empty);

            return new { userProfileData };
        }

        #region IdentityServer

        

        #endregion

        /// <summary>
        /// Authenticates user's selected profile
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        public async Task<object> AuthenticateAsync(Guid userId)
        {
            var user = await _userService.GetCurrentUserInfoAsync();

            if (user.Id == Guid.Empty)
            {
                _logger.LogError("Authentication error. User is not authenticated on Identity server. Parameters:" +
                                 " userId = {0}", userId.ToString());
                throw new AppException(_localizer["User is not authenticated on Identity server"], _localizer["Unauthorized"]);
            }

            var userProfileData = await _dataService.SingleAsync<UserAccountDto>(x =>
                    x.UserId == userId &&
                    x.AccountId == user.AccountId.ToString(),
                _userService.SystemUser);

            // TODO: Работу с выборкой организации переделать. Так как юзер может быть в нескольких, как сейвить пока не ясно
            _userService.UpdateCurrentUserInfo(userId, user.AccountId, string.Empty, string.Empty);

            return new { userProfileData };
        }

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
                throw new AppException(_localizer["User is not authenticated on Identity server"], _localizer["Unauthorized"]);
            }

            // TODO: Переписать комменты, и предвидеть возможно, переделать выборку по организациям
            var user = await _dataService.SingleOrDefaultAsync<UserLoginDto>(p => p.Id == currentUser.Id);
            if (user == null)
            {
                throw new AppException("Користувач не знайдений", _localizer["Unauthorized"]);
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
            }

            if (user.Id == Guid.Empty)
            {
                _logger.LogWarning("Security profile is not set to current user, try relogin. User id at identity server: {IdentityUserId}", user.AccountId);
                return new UserFullDto();
            }

            var data = await _dataService.FirstOrDefaultAsync<UserFullDto>(x =>
                x.UserId == user.Id,
                _userService.SystemUser);

            if (user is UserInfo userInfo)
            {
                data.Rights = userInfo.Rights;
            }

            return data;
        }

        public async Task Logout()
        {
            var userInfo = await _userService.GetCurrentUserInfoAsync();
            if (userInfo.LoginData.TryGetValue("AuthProvider", out var authProvider) && authProvider == "IdGovUa")
            {
                await HttpContextAccessor.HttpContext.SignOutAsync();
            }
            _userService.DeleteCurrentUserInfo();
        }
    }
}
