using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.System;
using Core.Data;
using Core.Mvc.Filters;
using Core.Security;
using Core.Services;
using Core.Services.Data;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace App.Business.Filters
{
    public class ApiAuthorizationFilter: IAsyncAuthorizationFilter
    {
        private readonly IUserInfoService _userService;
        private readonly ILogger<ApiAuthorizationFilter> _logger;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private static readonly ConcurrentDictionary<string, string> _controllerOperations = new ConcurrentDictionary<string, string>();
        private readonly ICommonDataService _commonDataService;

        public ApiAuthorizationFilter(IUserInfoService userService,
            ILogger<ApiAuthorizationFilter> logger,
            IStringLocalizer<SharedResource> localizer,
            ICommonDataService commonDataService)
        {
            _userService = userService;
            _logger = logger;
            _localizer = localizer;
            _commonDataService = commonDataService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            // Allow Anonymous skips all authorization
            //if (!context.Filters.Any(item => item is IAllowAnonymousFilter))
            if (!context.ActionDescriptor.EndpointMetadata.Any(item => item is Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute))
            {
                var userInfo = await _userService.GetCurrentUserInfoAsync();
                if (userInfo == null || string.IsNullOrEmpty(userInfo.AccountId))
                {
                    context.Result = new CustomUnauthorizedResult("User is not authorized or authorization period is expired", "Not authorized");
                    _logger.LogError(new EventId(LoggingEvents.AuthorizationError), "User id is empty. Check that user logged in on identity server. {AccountId}=", userInfo?.AccountId);
                }
                else
                {
                    var operationValidationErorr = await GetOperationValidationFailedError(context, userInfo);
                    if (!string.IsNullOrEmpty(operationValidationErorr))
                    {
                        context.Result = new CustomUnauthorizedResult(operationValidationErorr, _localizer["Operation denied"]);
                    }
                }
            }
        }

        private async Task<string> GetOperationValidationFailedError(AuthorizationFilterContext context, BaseUserInfo userInfo)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor actDescriptor)
            {
                var controllerName = actDescriptor.ControllerName;
                var actionName = actDescriptor.ActionName;
                var operationKey = controllerName + "." + actionName;
                if (!_controllerOperations.TryGetValue(operationKey, out var operationName)) {
                    var operationAttribute = actDescriptor.MethodInfo.GetCustomAttributes(typeof(OperationRightAttribute), true).FirstOrDefault() as OperationRightAttribute;
                    operationName = operationAttribute?.OperationName;
                    _controllerOperations.TryAdd(operationKey, operationName);
                }

                if (!string.IsNullOrEmpty(operationName))
                {
                    try
                    {
                        userInfo.AssertCanExecuteOperation(operationName);
                    }
                    catch (NoRightsException ex)
                    {
                        _logger.LogError(new EventId(LoggingEvents.OperationRigthsError), ex, "User with id={UserId}) has no rights to perform controller action {ControllerAction}", userInfo?.Id, context.ActionDescriptor.DisplayName);
                        
                        return string.Format(_localizer[operationName],string.Empty);
                    }
                }
            }

            return string.Empty;
        }
    }
}
