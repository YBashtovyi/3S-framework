using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.System;
using Core.Mvc.Controllers;
using Core.Security.Models;
using Core.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Api.Controllers
{
    [Route("api/user-profiles")]
    [ApiController]
    public class UserProfilesController: CommonApiController<UserProfileDetailDto, UserProfileDto, UserProfileListDto, UserProfile>
    {
        public UserProfilesController(ICommonDataService dataService, ILogger<UserProfilesController> logger) : base(dataService, logger)
        {
        }
    }
}
