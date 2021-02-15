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
    [Route("api/app-row-level-rights")]
    [ApiController]
    public class ApplicationRowLevelRightsController: CommonApiController<ApplicationRowLevelRightDetailDto, ApplicationRowLevelRightDto, ApplicationRowLevelRightListDto, ApplicationRowLevelRight>
    {
        public ApplicationRowLevelRightsController(ICommonDataService dataService, ILogger<ApplicationRowLevelRightsController> logger) : base(dataService, logger)
        {
        }
    }
}
