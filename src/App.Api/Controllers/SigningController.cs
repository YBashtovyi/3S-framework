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
    public class SigningSettingController: CommonApiController<CryptoSignFieldSettingDto, CryptoSignFieldSetting>
    {
        public SigningSettingController(ICommonDataService dataService, ILogger<SigningSettingController> logger) : base(dataService, logger)
        {
        }

    }
}
