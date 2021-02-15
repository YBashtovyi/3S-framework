using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.Prj;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace App.WebAPI.Controllers.prj
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectAdditionalAgreementController : CommonApiController<ProjectAdditionalAgreementDetailsDto, ProjectAdditionalAgreementEditDto, ProjectAdditionalAgreementListDto, ProjectAdditionalAgreement>
    {
        public ProjectAdditionalAgreementController(ICommonDataService dataService, ILogger<ProjectAdditionalAgreementController> logger) : base(dataService, logger)
        {
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<ProjectAdditionalAgreementEditDto>(id, null);
        }
    }
}
