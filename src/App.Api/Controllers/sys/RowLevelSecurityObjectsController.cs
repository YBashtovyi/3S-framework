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
    [Route("api/rls-objects")]
    [ApiController]
    public class RowLevelSecurityObjectsController: CommonApiController<RowLevelSecurityObjectDetailDto, RowLevelSecurityObjectDto, RowLevelSecurityObjectListDto, RowLevelSecurityObject>
    {
        public RowLevelSecurityObjectsController(ICommonDataService dataService, ILogger<RowLevelSecurityObjectsController> logger) : base(dataService, logger)
        {
        }
    }
}
