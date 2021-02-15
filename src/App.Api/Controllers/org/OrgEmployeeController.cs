using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.Org;
using App.Data.Models;
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
    public class OrgEmployeeController: CommonApiController<OrgEmployeeDto, OrgEmployeeDto, OrgEmployeeListDto, Employee>
    {
        public OrgEmployeeController(ICommonDataService dataService, ILogger<CommonApiController> logger) : base(dataService, logger)
        {
        }
    }
}
