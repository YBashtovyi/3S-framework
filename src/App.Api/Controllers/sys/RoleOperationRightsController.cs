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
    [Route("api/role-operation-rights")]
    [ApiController]
    public class RoleOperationRightsController: CommonApiController<RoleOperationRightDetailDto, RoleOperationRightDto, RoleOperationRightListDto, RoleOperationRight>
    {
        public RoleOperationRightsController(ICommonDataService dataService, ILogger<RoleOperationRightsController> logger) : base(dataService, logger)
        {
        }
    }
}
