using System;
using System.Threading.Tasks;
using App.Business.Services.PrjServices;
using App.Data.Dto.Prj;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static App.Business.Helpers.ControllerHelper;

namespace App.WebAPI.Controllers.prj
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectParticipantController: CommonApiController<ProjectParticipantDetailsDto, ProjectParticipantEditDto, ProjectParticipantListDto, ProjectParticipant>
    {
        public ProjectParticipantController(ICommonDataService dataService, ILogger<ProjectParticipantController> logger, ProjectParticipantService prjParticipantService) : base(dataService, logger)
        {
            _prjParticipantService = prjParticipantService;
            _logger = logger;
        }

        public readonly ProjectParticipantService _prjParticipantService;
        private readonly ILogger<ProjectParticipantController> _logger;

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<ProjectParticipantEditDto>(id, null);
        }

        public override async Task<IActionResult> PostItem(ProjectParticipantEditDto item)
        {
            try
            {
                return Ok(await _prjParticipantService.Create(item));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(PostItem));
                return BadRequest(badRequestDetails);
            }
        }

        public override async Task<IActionResult> PutItem(Guid id, ProjectParticipantEditDto item)
        {
            try
            {
                return Ok(await _prjParticipantService.Update(id, item));
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
                await _prjParticipantService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(DeleteItem));
                return BadRequest(badRequestDetails);
            }
        }

        #region Overridden unused api

        public override async Task<IActionResult> PatchItem(Guid id, JsonPatchDocument<ProjectParticipantEditDto> patchData)
        {
            return NotFound();
        }

        #endregion

    }
}
