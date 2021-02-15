using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProjectWorkScheduleController : CommonApiController<ProjectWorkScheduleDetailsDto, ProjectWorkScheduleEditDto, ProjectWorkScheduleListDto, ProjectWorkSchedule>
    {
        public ProjectWorkScheduleController(ICommonDataService dataService, ILogger<ProjectWorkScheduleController> logger, ProjectWorkScheduleService projectWorkScheduleService) : base(dataService, logger)
        {
            _logger = logger;
            _projectWorkScheduleService = projectWorkScheduleService;
        }

        private readonly ProjectWorkScheduleService _projectWorkScheduleService;
        private readonly ILogger<ProjectWorkScheduleController> _logger;

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<ProjectWorkScheduleEditDto>(id, null);
        }

        public override async Task<IActionResult> PostItem(ProjectWorkScheduleEditDto item)
        {
            try
            {
                return Ok(await _projectWorkScheduleService.Create(item));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(PostItem));
                return BadRequest(badRequestDetails);
            }
        }

        public override async Task<IActionResult> PutItem(Guid id, ProjectWorkScheduleEditDto item)
        {
            try
            {
                return Ok(await _projectWorkScheduleService.Edit(id, item));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(PutItem));
                return BadRequest(badRequestDetails);
            }
        }

        public override async Task<IActionResult> DeleteItem(Guid id, bool softDeleting = true)
        {
            try
            {
                await _projectWorkScheduleService.Delete(id);
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
