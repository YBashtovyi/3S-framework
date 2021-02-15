using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.Cdn;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.WebAPI.Controllers.cdn
{
    [Route("api/[controller]")]
    [Authorize]
    public class PositionController: CommonApiController<PositionDto, PositionDto, PositionListDto, Position>
    {
        public PositionController(ICommonDataService dataService, ILogger<PositionController> logger) : base(dataService, logger)
        {
        }
    }
}
