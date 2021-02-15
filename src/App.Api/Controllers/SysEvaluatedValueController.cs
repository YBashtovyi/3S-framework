using App.Data.Dto.System;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysEvaluatedValueController: CommonApiController<SysEvaluatedValueDto, SysEvaluatedValue>
    {
        public SysEvaluatedValueController(ICommonDataService dataService, ILogger<SysEvaluatedValueController> logger)
            : base(dataService, logger)
        {

        }
    }
}
