using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Business.Services.CdnServices;
using App.Data.Dto.Cdn;
using App.Data.Dto.System;
using App.Data.Models.cdn;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace App.WebAPI.Controllers.cdn
{
    [Route("api/[controller]")]
    [Authorize]
    public class WorkSubTypeController : CommonApiController<WorkSubTypeDetailsDto, WorkSubTypeEditDto, WorkSubTypeListDto, WorkSubType>
    {
        public WorkSubTypeController(ICommonDataService dataService, ILogger<WorkSubTypeController> logger, WorkSubTypeService workSubTypeService) : base(dataService, logger)
        {
            _workSubTypeService = workSubTypeService;
            _logger = logger;
        }

        private readonly WorkSubTypeService _workSubTypeService;
        private readonly ILogger<WorkSubTypeController> _logger;

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<WorkSubTypeEditDto>(id, null);
        }

        public override async Task<IActionResult> DeleteItem(Guid id, bool softDeleting = true)
        {
            return await Delete(id, softDeleting, _workSubTypeService.Delete);
        }

        [HttpPost("export/xlsx")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(415)]
        public async Task<IActionResult> DownloadListAsExcel(DownloadListModel options)
        {
            try
            {
                var (data, contentType, fileName) = await _workSubTypeService.DownloadListAsExcelAsync(options);
                return File(data, contentType, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while downloading files.");
                var badRequestDetails = new ProblemDetails { Status = 400, Title = "Bad request", Detail = $"{ex.Message}" };
                return BadRequest(badRequestDetails);
            }
        }
    }
}
