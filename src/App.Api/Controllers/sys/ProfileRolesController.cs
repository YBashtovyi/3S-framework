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
    [Route("api/profile-roles")]
    [ApiController]
    public class ProfileRolesController: CommonApiController<ProfileRoleDetailDto, ProfileRoleDto, ProfileRoleListDto, ProfileRole>
    {
        public ProfileRolesController(ICommonDataService dataService, ILogger<ProfileRolesController> logger) : base(dataService, logger)
        {
        }
    }
}
