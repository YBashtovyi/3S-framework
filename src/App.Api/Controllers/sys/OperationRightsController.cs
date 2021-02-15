using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.System;
using App.Data.Helpers;
using Core.Mvc.Controllers;
using Core.Security.Models;
using Core.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Api.Controllers
{
    [Route("api/operation-rights")]
    [ApiController]
    public class OperationRightsController: CommonApiController<OperationRightDetailDto, OperationRightDto, OperationRightListDto, OperationRight>
    {
        public OperationRightsController(ICommonDataService dataService, ILogger<OperationRightsController> logger) : base(dataService, logger)
        {
        }

        [HttpGet("operations")]
        public ActionResult<IEnumerable<ApplicationOperations.ApplicationOperationData>> GetApplicationOperations()
        {
            return Ok(ApplicationOperations.GetDeclaredOperations());
        }
    }
}
