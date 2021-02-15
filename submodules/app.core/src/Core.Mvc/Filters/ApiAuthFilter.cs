using System;
using System.Linq;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Mvc.Filters
{
    public class ApiAuthFilter: IAuthorizationFilter
    {
        private readonly IUserInfoService _userService;

        public ApiAuthFilter(IUserInfoService userService)
        {
            _userService = userService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Allow Anonymous skips all authorization
            if (!context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                var userInfo = _userService.GetCurrentUserInfo();
                if (userInfo == null || userInfo.Id == Guid.Empty)
                {
                    context.Result = new CustomUnauthorizedResult("Authorized user has no permissions to perform this operation", "Operation denied");
                }
            }
        }
    }

    public class CustomUnauthorizedResult: JsonResult
    {
        public CustomUnauthorizedResult(string message, string title)
            : base(new ProblemDetails { Title = title, Detail = message, Status = 403 })
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }
    public class CustomError
    {
        public string Error { get; }

        public CustomError(string message)
        {
            Error = message;
        }
    }

}
