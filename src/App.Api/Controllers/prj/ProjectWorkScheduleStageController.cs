using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using App.Business.Services.PrjServices;
using App.Data.Dto.Prj;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using static App.Business.Helpers.ControllerHelper;

namespace App.WebAPI.Controllers.prj
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectWorkScheduleStageController : CommonApiController<ProjectWorkScheduleStageDetailsDto, ProjectWorkScheduleStageEditDto, ProjectWorkScheduleStageListDto, ProjectWorkScheduleStage>
    {
        public ProjectWorkScheduleStageController(ICommonDataService dataService, ILogger<ProjectWorkScheduleStageController> logger, ProjectWorkScheduleStageService projectWorkScheduleStageService) : base(dataService, logger)
        {
            _logger = logger;
            _projectWorkScheduleStageService = projectWorkScheduleStageService;
        }

        private readonly ILogger<ProjectWorkScheduleStageController> _logger;
        private readonly ProjectWorkScheduleStageService _projectWorkScheduleStageService;

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<ProjectWorkScheduleStageEditDto>(id, null);
        }

        public override async Task<IActionResult> DeleteItem(Guid id, bool softDeleting = true)
        {
            try
            {
                await _projectWorkScheduleStageService.Delete(id, softDeleting);
                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(DeleteItem));
                return BadRequest(badRequestDetails);
            }
        }
    }
}
