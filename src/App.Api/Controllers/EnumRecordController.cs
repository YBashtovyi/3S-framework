using System;
using System.Threading.Tasks;
using App.Business.Services.ApiControllerServices;
using App.Data.Dto.Common;
using App.Data.Models;
using Core.Mvc.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static App.Business.Helpers.ControllerHelper;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumRecordController: CommonApiController<EnumRecordDto, EnumRecordDto, EnumRecordListDto, EnumRecord>
    {
        private readonly EnumRecordService _service;
        private readonly ILogger<EnumRecordController> _logger;

        public EnumRecordController(EnumRecordService service, ILogger<EnumRecordController> logger) : base(service.DataService, logger)
        {
            _service = service;
        }

        public override async Task<IActionResult> PutItem(Guid id, EnumRecordDto item)
        {
            return await Update<EnumRecordDto, EnumRecord>(id, item, _service.Update);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EnumRecordDto dto)
        {
            try
            {
                return Ok(await _service.Create(dto));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(Create));
                return BadRequest(badRequestDetails);
            }
        }
    }
}
