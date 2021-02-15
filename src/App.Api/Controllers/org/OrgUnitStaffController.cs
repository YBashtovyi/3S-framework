using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Business.Services.AdministrationServices;
using App.Business.Services.OrganizationServices;
using App.Data.Dto.Org;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.WebAPI.Controllers.org
{
    [Route("api/[controller]")]
    [Authorize]
    public class OrgUnitStaffController: CommonApiController<OrgUnitStaffDetailsDto, OrgUnitStaffEditDto, OrgUnitStaffListDto, OrgUnitStaff>
    {
        public OrgUnitStaffController(ICommonDataService dataService, ILogger<OrgUnitStaffController> logger, OrgUnitStaffService orgUnitStaffService) : base(dataService, logger)
        {
            _orgUnitStaffService = orgUnitStaffService;
        }

        private readonly OrgUnitStaffService _orgUnitStaffService;

        [HttpPost]
        public override async Task<IActionResult> PostItem(OrgUnitStaffEditDto item)
        {
            try
            {
                return Ok( await _orgUnitStaffService.CreateOrgUnitStaff(item, true));
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, "Помилка під час створення посади", ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<OrgUnitStaffEditDto>(id, null);
        }
    }
}
