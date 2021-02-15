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
    [Route("api/row-level-rights")]
    [ApiController]
    public class RowLevelRightsController: CommonApiController<RowLevelRightDetailDto, RowLevelRightDto, RowLevelRightListDto, RowLevelRight>
    {
        public RowLevelRightsController(ICommonDataService dataService, ILogger<RowLevelRightsController> logger) : base(dataService, logger)
        {
        }
    }
}
