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
    [Route("api/role-rights")]
    [ApiController]
    public class RoleRightsController: CommonApiController<RoleRightDetailDto, RoleRightDto, RoleRightListDto, RoleRight>
    {
        public RoleRightsController(ICommonDataService dataService, ILogger<RoleRightsController> logger) : base(dataService, logger)
        {
        }
    }
}
