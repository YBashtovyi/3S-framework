using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Business.Services.OrganizationServices;
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
    public class DepartmentController: CommonApiController<DepartmentDetailsDto, DepartmentEditDto, DepartmentListDto, Department>
    {
        public DepartmentController(ICommonDataService dataService, ILogger<DepartmentController> logger) : base(dataService, logger)
        {
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<DepartmentEditDto>(id, null);
        }
    }
}
