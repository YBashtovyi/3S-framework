using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.Administration;
using Core.Administration.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.WebAPI.Controllers.adm
{
    [Route("api/[controller]")]
    [Authorize]
    public class RightController : CommonApiController<RightDetailsDto, RightEditDto, RightListDto, Right>
    {
        public RightController(ICommonDataService dataService, ILogger<RightController> logger) : base(dataService, logger)
        {
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<RightEditDto>(id, null);
        }
    }
}
