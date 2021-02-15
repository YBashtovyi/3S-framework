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
    [Route("api/profile-rights")]
    [ApiController]
    public class ProfileRightsController: CommonApiController<ProfileRightDetailDto, ProfileRightDto, ProfileRightListDto, ProfileRight>
    {
        public ProfileRightsController(ICommonDataService dataService, ILogger<ProfileRightsController> logger) : base(dataService, logger)
        {
        }
    }
}
