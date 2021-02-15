using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Business.Services.PrjServices;
using App.Data.Dto.Common.NotMapped;
using App.Data.Dto.Prj;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static App.Business.Helpers.ControllerHelper;

namespace App.WebAPI.Controllers.prj
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectPhotoReportController : CommonApiController<ProjectPhotoReportDetailsDto, ProjectPhotoReportEditDto, ProjectPhotoReportListDto, ProjectPhotoReport>
    {
        public ProjectPhotoReportController(ICommonDataService dataService, ILogger<ProjectPhotoReportController> logger, ProjectPhotoReportService photoReportService) : base(dataService, logger)
        {
            _logger = logger;
            _photoReportService = photoReportService;
        }

        private readonly ILogger<ProjectPhotoReportController> _logger;
        private readonly ProjectPhotoReportService _photoReportService;

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<ProjectPhotoReportEditDto>(id, null);
        }

        public override async Task<IActionResult> GetItemExt(Guid id)
        {
            var photoReport = await DataService.SingleOrDefaultAsync<ProjectPhotoReportDetailsDto>(p => p.Id == id);
            if (photoReport == null)
            {
                return Ok();
            }

            if (string.IsNullOrEmpty(photoReport.AtuCoordinates))
            {
                return Ok(photoReport);
            }

            photoReport.AtuCoordinateList = JsonConvert.DeserializeObject<MapCoordinate>(photoReport.AtuCoordinates);

            return Ok(photoReport);
        }

        public override async Task<IActionResult> PostItem(ProjectPhotoReportEditDto item)
        {
            try
            {
                return Ok(await _photoReportService.Create(item));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(PostItem));
                return BadRequest(badRequestDetails);
            }
        }

        #region ATU Coordinate

        /// <summary>
        /// Add coordinate to object
        /// </summary>
        /// <param name="prjPhotoReportId">PhotoReportId</param>
        /// <param name="coordinate">Coordinate object</param>
        /// <returns>Returns a list of object coordinates</returns>
        [HttpPost("add-atu-coordinate/{prjPhotoReportId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCoordinate(Guid prjPhotoReportId, MapCoordinate coordinate)
        {
            try
            {
                return Ok(await _photoReportService.AddCoordinate(prjPhotoReportId, coordinate));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(AddCoordinate));
                return BadRequest(badRequestDetails);
            }
        }

        [HttpGet("get-atu-coordinate/{prjPhotoReportId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCoordinates(Guid prjPhotoReportId)
        {
            try
            {
                return Ok(await _photoReportService.GetCoordinates(prjPhotoReportId));
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
