using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.Atu;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AtuController: CommonApiController
    {
        public AtuController(ICommonDataService dataService, ILogger<AtuController> logger) : base(dataService, logger)
        {
        }

        [HttpGet("get-region-list")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRegionList([FromQuery] IDictionary<string, string> paramList)
        {
            return await List<RegionListDto>(paramList, null);
        }
    }
}
