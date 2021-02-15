﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.System;
using Core.Mvc.Controllers;
using Core.Security.Models;
using Core.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Api.Controllers
{
    [Route("api/field-rights")]
    [ApiController]
    public class FieldRightsController: CommonApiController<FieldRightDetailDto, FieldRightDto, FieldRightListDto, FieldRight>
    {
        public FieldRightsController(ICommonDataService dataService, ILogger<FieldRightsController> logger) : base(dataService, logger)
        {
        }
    }
}
