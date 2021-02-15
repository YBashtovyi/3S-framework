using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Business.Services.AdministrationServices;
using App.Data.Dto.Administration;
using App.Data.Dto.Administration.NotMapped;
using App.Data.Dto.Org;
using Core.Administration.Helpers;
using Core.Administration.Models;
using Core.Base.Administration;
using Core.Mvc.Controllers;
using Core.Services;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static App.Business.Helpers.ControllerHelper;

namespace App.WebAPI.Controllers.adm
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : CommonApiController<UserDetailsDto, UserDetailsDto, UserListDto, User>
    {
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ICommonDataService dataService, ILogger<UserController> logger, UserService userService) : base(dataService, logger)
        {
            _userService = userService;
            _logger = logger;
        }

        #region RLS

        [HttpGet("get-user-rls/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRls(Guid userId)
        {
            try
            {
                return Ok( await _userService.GetRls(userId));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(UserController)}.{nameof(GetRls)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpPost("add-rls/{userId}")]
        public async Task<IActionResult> AddRls(Guid userId, string rlsType, string crud, [FromBody] List<RowLevelSecurityItemViewModel> rls)
        {
            try
            {
                return Ok(await _userService.AddRls(userId, rlsType, crud, rls));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(UserController)}.{nameof(AddRls)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpPut("edit-rls/{userId}")]
        public async Task<IActionResult> EditRls(Guid userId, string rlsType, string crud, [FromBody] RowLevelSecurityItemViewModel rls)
        {
            try
            {
                return Ok(await _userService.EditRls(userId, rlsType, crud, rls));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(UserController)}.{nameof(EditRls)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpDelete("delete-rls/{userId}")]
        public async Task<IActionResult> DeleteRls(Guid userId, string rlsType, Guid rlsId)
        {
            try
            {
                await _userService.DeleteRls(userId, rlsType, rlsId);

                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(UserController)}.{nameof(DeleteRls)}");
                return BadRequest(badRequestDetails);
            }
        }

        #endregion

        #region Roles

        [HttpGet("get-user-roles/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRoles(Guid userId)
        {
            try
            {
                return Ok(await _userService.GetRoles(userId));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(UserController)}.{nameof(GetRoles)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpPost("add-role-to-user/{userId}")]
        public async Task<IActionResult> AddRoleToUser(Guid userId, [FromBody] List<Guid> roleList)
        {
            try
            {
                await _userService.AddRoleToUser(userId, roleList);

                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(UserController)}.{nameof(AddRoleToUser)}");
                return BadRequest(badRequestDetails);
            }
        }

        [HttpDelete("delete-role-from-user/{userId}")]
        public async Task<IActionResult> DeleteRoleFromUser(Guid userId, Guid roleId)
        {
            try
            {
                await _userService.DeleteRoleFromUser(userId, roleId);

                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, $"{nameof(UserController)}.{nameof(DeleteRoleFromUser)}");
                return BadRequest(badRequestDetails);
            }
        }

        #endregion

       
    }
}
