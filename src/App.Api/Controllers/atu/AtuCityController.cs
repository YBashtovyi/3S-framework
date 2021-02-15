using System;
using System.Threading.Tasks;
using App.Data.Dto.Atu;
using App.Data.Models;
using App.Business.Services.CityServices;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace App.WebAPI.Controllers.org
{
    [Route("api/[controller]")]
    [Authorize]
    public class CityController: CommonApiController<CityDetailsDto, CityEditDto, CityListDto, City>
    {
        public CityController(ICommonDataService dataService, ILogger<CityController> logger, CityService CityService) : base(dataService, logger)
        {
            _cityService = CityService;
        }

        private CityService _cityService { get; }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<CityEditDto>(id, null);
        }
    }
}
