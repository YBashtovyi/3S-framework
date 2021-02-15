using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.Org;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.WebAPI.Controllers.org
{
    [Route("api/[controller]")]
    [Authorize]
    public class OrgUnitPositionController: CommonApiController<OrgUnitPositionDetailsDto, OrgUnitPositionEditDto, OrgUnitPositionListDto, OrgUnitPosition>
    {
        public OrgUnitPositionController(ICommonDataService dataService, ILogger<OrgUnitPositionController> logger) : base(dataService, logger)
        {
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<OrgUnitPositionEditDto>(id, null);
        }

        public override Task<IActionResult> DeleteItem(Guid id, bool softDeleting = false)
        {
            return base.DeleteItem(id, softDeleting);
        }
    }
}
