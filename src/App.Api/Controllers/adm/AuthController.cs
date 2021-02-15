using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using App.Business.Services.ApplicationServices;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using static App.Business.Helpers.ControllerHelper;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AuthController : CommonApiController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AuthService _authService;
        private readonly IdentityService _identityService;
        private readonly IStringLocalizer<AuthController> _localizer;

        public AuthController(ICommonDataService dataService, ILogger<AuthController> logger, AuthService authService, IStringLocalizer<AuthController> localizer, IdentityService identityService) : base(dataService, logger)
        {
            _logger = logger;
            _authService = authService;
            _localizer = localizer;
            _identityService = identityService;
        }

        #region IdGovUa

        [HttpGet("authenticate-id-gov-ua")]
        [AllowAnonymous]
        public IActionResult AuthenticateIdGovUa()
        {
            try
            {
                return Ok(_authService.GetAuthIdGovUaUrl());
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(AuthController)}.{nameof(AuthenticateIdGovUa)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpPost("id-gov-ua-code")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthorizeIdGovUa(string state, string code)
        {
            try
            {
                return Ok(await _authService.AuthenticateIdGovUa(state, code));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(AuthController)}.{nameof(AuthorizeIdGovUa)}");
                return BadRequest(badRequestDetails);
            }
        }

        #endregion

        #region IdentityServer

        [HttpPost("try-authenticate/")]
        [ProducesResponseType(100)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> TryAuthenticate()
        {
            try
            {
                var userData = await _authService.GetUserDataAsync();

                if (userData == null)
                {
                    var badRequestDetails = CreateProblemDetails(401,
                        _localizer["Unauthorized"],
                        _localizer["User is not unauthorized"],
                        _logger,
                        "Error occurred while trying authenticate user in method {MethodName}. Application profile is not found. User id on identity server: {UserId}",
                        $"{nameof(AuthController)}.{nameof(TryAuthenticate)}", _authService.CurrentUser.AccountId);
                    return Unauthorized(badRequestDetails);
                }

                return await Authenticate(userData.Id);

            }
            catch (Exception ex)
            {
                var problemDetails = CreateProblemDetails(ex, 401);
                return Unauthorized(problemDetails);
            }
        }

        [HttpPost("authenticate/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(Guid userId)
        {
            try
            {
                var userData = await _authService.AuthenticateAsync(userId);
                return Ok(userData);
            }
            catch (Exception ex)
            {
                ProblemDetails badRequestDetails;
                var methodName = $"{nameof(AuthController)}.{nameof(Authenticate)}";
                if (userId == Guid.Empty)
                {
                    badRequestDetails = CreateProblemDetails(ex,
                        _logger,
                        "Error occurred while trying authenticating user in method {methodName}. userId is empty",
                        methodName);
                }
                else
                {
                    badRequestDetails = CreateProblemDetails(ex,
                        _logger,
                        "Error occurred while trying authenticating user in method {methodName} with user id {userId}",
                        methodName, userId.ToString());
                }

                return BadRequest(badRequestDetails);
            }
        }

        [HttpGet("getIdentityUsers")]
        public async Task<IActionResult> GetIdentityUsers()
        {
            await _identityService.GetAccounts();
            return Ok();
        }

        #endregion

        /// <summary>
        /// Returns current user info with security profile if set
        /// </summary>
        /// <remarks>
        /// Method is annonymous because frontend defines its own logic based on result of this method
        /// If method returns null, then token is not set
        /// If method returns empty model, then user is not logged in with security profile
        /// </remarks>
        /// <returns></returns>
        [HttpGet("info")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserFullDto()
        {
            try
            {
                var data = await _authService.GetUserFullDtoAsync();
                if (data == null)
                {
                    return Unauthorized(CreateProblemDetails(401, "Не знайдено", "Користувач не авторизований"));
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex,
                    _logger,
                    $"{nameof(AuthController)}.{nameof(GetUserFullDto)}");
                return BadRequest(badRequestDetails);
            }
        }

        /// <summary>
        /// Removes all cached user data needed for work. Then you should call login to continue work
        /// </summary>
        /// <returns>No content</returns>
        /// <response code="200">If success</response>
        [HttpGet("logout/")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                await _authService.Logout();
                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex,
                    400,
                    _localizer["Bad request"],
                    _localizer["Logout error"],
                    _logger,
                    "Error occurred while logging off user in method {MethodName}",
                    $"{nameof(AuthController)}.{nameof(LogoutAsync)}");
                return BadRequest(badRequestDetails);
            }
        }
    }
}
