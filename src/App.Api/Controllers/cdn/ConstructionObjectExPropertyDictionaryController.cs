using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Business.Services.CdnServices;
using App.Data.Dto.Cdn;
using App.Data.Dto.System;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace App.WebAPI.Controllers.cdn
{
    [Route("api/[controller]")]
    [Authorize]
    public class ConstructionObjectExPropertyDictionaryController 
        : CommonApiController<
            ConstructionObjectExPropertyDictionaryDetailsDto,
            ConstructionObjectExPropertyDictionaryEditDto,
            ConstructionObjectExPropertyDictionaryListDto,
            ConstructionObjectExPropertyDictionary>
    {
        private readonly ConstructionObjectExPropertyDictionaryService _exPropertyDictionaryService;
        private readonly ILogger<ConstructionObjectExPropertyDictionaryController> _logger;

        public ConstructionObjectExPropertyDictionaryController(ICommonDataService dataService, ILogger<ConstructionObjectExPropertyDictionaryController> logger, ConstructionObjectExPropertyDictionaryService exPropertyDictionaryService) : base(dataService, logger)
        {
            _exPropertyDictionaryService = exPropertyDictionaryService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<ConstructionObjectExPropertyDictionaryEditDto>(id, null);
        }

        public override async Task<IActionResult> DeleteItem(Guid id, bool softDeleting = true)
        {
            return await Delete(id, softDeleting, _exPropertyDictionaryService.Delete);
        }

        [HttpPost("export/xlsx")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(415)]
        public async Task<IActionResult> DownloadListAsExcel(DownloadListModel options)
        {
            try
            {
                var (data, contentType, fileName) = await _exPropertyDictionaryService.DownloadListAsExcelAsync(options);
                return File(data, contentType, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while downloading files.");
                var badRequestDetails = new ProblemDetails { Status = 400, Title = "Bad request", Detail = $"{ex.Message}" };
                return BadRequest(badRequestDetails);
            }
        }

        [HttpGet("get-type-of-object-list")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTypeOfObjectList()
        {
            try
            {
                return Ok(await DataService.GetDtoAsync<ConstructionObjectTypeOfObjectListDto>(p => p.ParentId == null));
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
    }
}
