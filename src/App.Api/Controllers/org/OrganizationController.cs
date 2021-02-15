using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Business.Services.OrganizationServices;
using App.Data.Dto.Cdn;
using App.Data.Dto.Org;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static App.Business.Helpers.ControllerHelper;

namespace App.WebAPI.Controllers.org
{
    [Route("api/[controller]")]
    [Authorize]
    public class OrganizationController: CommonApiController<OrganizationDetailsDto, OrganizationEditDto, OrganizationListDto, Organization>
    {
        public OrganizationController(ICommonDataService dataService, ILogger<OrganizationController> logger, OrganizationService OrgService) : base(dataService, logger)
        {
            _orgService = OrgService;
        }

        private OrganizationService _orgService { get; }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<OrganizationEditDto>(id, null);
        }

        #region Extended Property

        [HttpGet("get-extended-property/{orgId}")]
        public async Task<IActionResult> GetExtendedPropertyList(Guid orgId, [FromQuery] IDictionary<string, string> paramList)
        {
            paramList.Add("OrgUnitId", orgId.ToString());
            return await List<OrgUnitExtendedPropertyListDto>(paramList, null);
        }

        [HttpGet("get-extended-property-by-id/{propId}")]
        public async Task<IActionResult> GetExtendedPropertyById(Guid propId)
        {
            return await Details<OrgUnitExtendedPropertyListDto>(propId, null);
        }

        [HttpPost("add-extended-property")]
        public async Task<IActionResult> AddExtendedProperty(OrgUnitExtendedPropertyListDto dto)
        {
            return await Create<OrgUnitExtendedPropertyListDto, OrgUnitExtendedProperty>(dto, nameof(AddExtendedProperty), null);
        }

        [HttpPut("edit-extended-property/{propId}")]
        public async Task<IActionResult> EditExtendedProperty(Guid propId, OrgUnitExtendedPropertyListDto dto)
        {
            return await Update<OrgUnitExtendedPropertyListDto, OrgUnitExtendedProperty>(propId, dto, null);
        }

        [HttpDelete("delete-extended-property/{propId}")]
        public async Task<IActionResult> DeleteExtendedProperty(Guid propId)
        {
            return await Delete<OrgUnitExtendedProperty>(propId, false, null);
        }

        #endregion
    }
}
