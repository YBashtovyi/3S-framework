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
    public class RightsController: CommonApiController<RightDetailDto, RightDto, RightListDto, Right>
    {
        public RightsController(ICommonDataService dataService, ILogger<RightsController> logger) : base(dataService, logger)
        {
        }
    }
}
