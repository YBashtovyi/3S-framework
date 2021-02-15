using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Business.Services.AdministrationServices;
using App.Data.Dto.Administration;
using App.Data.Dto.Administration.NotMapped;
using Core.Administration.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static App.Business.Helpers.ControllerHelper;

namespace App.WebAPI.Controllers.adm
{
    [Route("api/[controller]")]
    [Authorize]
    public class RoleController : CommonApiController<RoleDetailsDto, RoleEditDto, RoleListDto, Role>
    {
        public RoleController(ICommonDataService dataService, ILogger<RoleController> logger, RoleService roleService) : base(dataService, logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        private readonly RoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<RoleEditDto>(id, null);
        }

        [HttpGet("role-right/{roleId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRoleRight(Guid roleId)
        {
            try
            {
                return Ok( await _roleService.GetRoleRight(roleId));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(RoleController)}.{nameof(GetRoleRight)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpPost("role-right/{roleId}")]
        public async Task<IActionResult> CreateRoleRight(Guid roleId, List<Guid> rightIds)
        {
            try
            {
                await _roleService.CreateRoleRight(roleId, rightIds);
                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(RoleController)}.{nameof(CreateRoleRight)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpDelete("role-right/{roleRightId}")]
        public async Task<IActionResult> DeleteRoleRight(Guid roleRightId)
        {
            try
            {
                await _roleService.DeleteRoleRight(roleRightId);
                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(RoleController)}.{nameof(DeleteRoleRight)}");
                return BadRequest(badRequestDetails);
            }
        }

        #region RLS

        [HttpGet("get-user-rls/{roleId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRls(Guid roleId)
        {
            try
            {
                return Ok(await _roleService.GetRls(roleId));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(RoleController)}.{nameof(GetRls)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpPost("add-rls/{roleId}")]
        public async Task<IActionResult> AddRls(Guid roleId, string rlsType, string crud, [FromBody] List<RowLevelSecurityItemViewModel> rls)
        {
            try
            {
                return Ok(await _roleService.AddRls(roleId, rlsType, crud, rls));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(RoleController)}.{nameof(AddRls)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpPut("edit-rls/{roleId}")]
        public async Task<IActionResult> EditRls(Guid roleId, string rlsType, string crud, [FromBody] RowLevelSecurityItemViewModel rls)
        {
            try
            {
                return Ok(await _roleService.EditRls(roleId, rlsType, crud, rls));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(RoleController)}.{nameof(EditRls)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpDelete("delete-rls/{roleId}")]
        public async Task<IActionResult> DeleteRls(Guid roleId, string rlsType, Guid rlsId)
        {
            try
            {
                await _roleService.DeleteRls(roleId, rlsType, rlsId);

                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(RoleController)}.{nameof(DeleteRls)}");
                return BadRequest(badRequestDetails);
            }
        }

        #endregion
    }
}
