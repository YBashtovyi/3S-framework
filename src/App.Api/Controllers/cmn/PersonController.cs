using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Business;
using App.Data.Dto.Cdn;
using App.Data.Dto.Common;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.WebAPI.Controllers.cmn
{
    [Route("api/[controller]")]
    [Authorize]
    public class PersonController: CommonApiController<PersonDetailsDto, PersonEditDto, PersonListDto, Person>
    {
        public PersonController(ICommonDataService dataService, ILogger<PersonController> logger) : base(dataService, logger)
        {
        }

        public override Task<IActionResult> PostItem(PersonEditDto item)
        {
            item.Caption = $"{item.LastName} {item.FirstName} {item.MiddleName}";
            item.Gender = Constants.Gender.NotSpecified;
            item.IdentityDocument = Constants.IdentityDocument.NotSpecified;

            return base.PostItem(item);
        }

        public override Task<IActionResult> PutItem(Guid id, PersonEditDto item)
        {
            item.Caption = $"{item.LastName} {item.FirstName} {item.MiddleName}";

            return base.PutItem(id, item);
        }

        #region Extended Property

        [HttpGet("get-extended-property/{personId}")]
        public async Task<IActionResult> GetExtendedPropertyList(Guid personId, [FromQuery] IDictionary<string, string> paramList)
        {
            paramList.Add("PersonId", personId.ToString());
            return await List<PersonExtendedPropertyListDto>(paramList, null);
        }

        [HttpGet("get-extended-property-by-id/{propId}")]
        public async Task<IActionResult> GetExtendedPropertyById(Guid propId)
        {
            return await Details<PersonExtendedPropertyListDto>(propId, null);
        }

        [HttpPost("add-extended-property")]
        public async Task<IActionResult> AddExtendedProperty(PersonExtendedPropertyListDto dto)
        {
            return await Create<PersonExtendedPropertyListDto, PersonExtendedProperty>(dto, nameof(AddExtendedProperty), null);
        }

        [HttpPut("edit-extended-property/{propId}")]
        public async Task<IActionResult> EditExtendedProperty(Guid propId, PersonExtendedPropertyListDto dto)
        {
            return await Update<PersonExtendedPropertyListDto, PersonExtendedProperty>(propId, dto, null);
        }

        [HttpDelete("delete-extended-property/{propId}")]
        public async Task<IActionResult> DeleteExtendedProperty(Guid propId)
        {
            return await Delete<PersonExtendedProperty>(propId, false, null);
        }

        #endregion
    }
}
