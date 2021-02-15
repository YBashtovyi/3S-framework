using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Business.Services.PrjServices;
using App.Data.Dto.Cdn;
using App.Data.Dto.Common.NotMapped;
using App.Data.Dto.Prj;
using App.Data.Dto.System;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static App.Business.Helpers.ControllerHelper;

namespace App.WebAPI.Controllers.prj
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController: CommonApiController<ProjectDetailsDto, ProjectEditDto ,ProjectListDto, Project>
    {
        public ProjectController(ICommonDataService dataService, ILogger<ProjectController> logger, ProjectService ProjectService) : base(dataService, logger)
        {
            _projectService = ProjectService;
            _logger = logger;
        }

        private readonly ProjectService _projectService;
        private readonly ILogger<ProjectController> _logger;

        public override async Task<IActionResult> GetItemExt(Guid id)
        {
            var project = await DataService.SingleOrDefaultAsync<ProjectDetailsDto>(p => p.Id == id);
            if (project == null)
            {
                return Ok();
            }

            return Ok(project);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<ProjectEditDto>(id, null);
        }

        public override async Task<IActionResult> PostItem(ProjectEditDto item)
        {
            try
            {
                return Ok(await _projectService.Create(item));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(PostItem));
                return BadRequest(badRequestDetails);
            }
        }

        public override async Task<IActionResult> PutItem(Guid id, ProjectEditDto item)
        {
            try
            {
                await _projectService.Edit(id, item);
                return NoContent();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(PutItem));
                return BadRequest(badRequestDetails);
            }
        }

        public override async Task<IActionResult> DeleteItem(Guid id, bool softDeleting = true)
        {
            return await Delete(id, softDeleting, _projectService.Delete);
        }

        [HttpPost("export/xlsx")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(415)]
        public async Task<IActionResult> DownloadListAsExcel(DownloadListModel options)
        {
            try
            {
                var (data, contentType, fileName) = await _projectService.DownloadListAsExcelAsync(options);
                return File(data, contentType, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while downloading files.");
                var badRequestDetails = new ProblemDetails { Status = 400, Title = "Bad request", Detail = $"{ex.Message}" };
                return BadRequest(badRequestDetails);
            }
        }

        [HttpGet("get-type-of-project-work-list")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTypeOfProjectWorkList([FromQuery] IDictionary<string, string> paramList)
        {
            return await List<TypeOfObjectWorkListDto>(paramList, null);
        }

        [HttpGet("get-project-participant-employee-list")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProjectParticipantEmployeeList(
            [FromQuery] IDictionary<string, string> paramList)
        {
            return await List<ProjectParticipantEmployeeListDto>(paramList, null);
        }

        #region ATU Coordinate

        /// <summary>
        /// Add coordinate to object
        /// </summary>
        /// <param name="prjId">ProjectId</param>
        /// <param name="coordinate">Coordinate object</param>
        /// <returns>Returns a list of object coordinates</returns>
        [HttpPost("add-atu-coordinate/{prjId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCoordinate(Guid prjId, MapCoordinate coordinate)
        {
            try
            {
                return Ok(await _projectService.AddCoordinate(prjId, coordinate));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(AddCoordinate));
                return BadRequest(badRequestDetails);
            }
        }

        [HttpGet("get-atu-coordinate/{prjId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCoordinates(Guid prjId)
        {
            try
            {
                return Ok(await _projectService.GetCoordinates(prjId));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(GetCoordinates));
                return BadRequest(badRequestDetails);
            }
        }

        #endregion
    }
}
