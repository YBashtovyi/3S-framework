using System;
using System.Threading.Tasks;
using App.Data.Dto.Atu;
using App.Data.Models;
using App.Business.Services.CountryServices;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace App.WebAPI.Controllers.org
{
    [Route("api/[controller]")]
    [Authorize]
    public class CountryController: CommonApiController<CountryDetailsDto, CountryEditDto, CountryListDto, Country>
    {
        public CountryController(ICommonDataService dataService, ILogger<CountryController> logger, CountryService countryService) : base(dataService, logger)
        {
            _countryService = countryService;
        }

        private CountryService _countryService { get; }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<CountryEditDto>(id, null);
        }
    }
}
