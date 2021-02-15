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
    public class ProjectContractController : CommonApiController<ProjectContractDetailsDto, ProjectContractEditDto, ProjectContractAddAgreementListDto, ProjectContract>
    {
        public ProjectContractController(ICommonDataService dataService, ILogger<ProjectContractController> logger, ProjectContractService contractService) : base(dataService, logger)
        {
            _logger = logger;
            _contractService = contractService;
        }

        private readonly ProjectContractService _contractService;
        private readonly ILogger<ProjectContractController> _logger;

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<ProjectContractEditDto>(id, null);
        }

        public override async Task<IActionResult> PostItem(ProjectContractEditDto item)
        {
            try
            {
                return Ok(await _contractService.Create(item));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(PostItem));
                return BadRequest(badRequestDetails);
            }
        }

        public override async Task<IActionResult> DeleteItem(Guid id, bool softDeleting = true)
        {
            try
            {
                await _contractService.Delete(id);
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
