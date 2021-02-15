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
    [Route("api/profile-operation-rights")]
    [ApiController]
    public class ProfileOperationRightsController: CommonApiController<ProfileOperationRightDetailDto, ProfileOperationRightDto, ProfileOperationRightListDto, ProfileOperationRight>
    {
        public ProfileOperationRightsController(ICommonDataService dataService, ILogger<ProfileOperationRightsController> logger) : base(dataService, logger)
        {
        }
    }
}
