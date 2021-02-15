using System;
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
    [Route("api/user-defaults")]
    [ApiController]
    public class UserDefaultsController: CommonApiController<UserDefaultValueDetailDto, UserDefaultValueDto, UserDefaultValueListDto, UserDefaultValue>
    {
        public UserDefaultsController(ICommonDataService dataService, ILogger<UserDefaultsController> logger) : base(dataService, logger)
        {
        }
    }
}
