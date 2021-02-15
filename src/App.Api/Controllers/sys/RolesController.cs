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
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController: CommonApiController<RoleDetailDto, RoleDto, RoleListDto, Role>
    {
        public RolesController(ICommonDataService dataService, ILogger<RolesController> logger) : base(dataService, logger)
        {
        }
    }
}
