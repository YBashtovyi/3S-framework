using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Business.Services.ApplicationServices;
using App.Data.Dto.NotMappedDto;
using Core.Common.Helpers;
using Core.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static App.Business.Helpers.ControllerHelper;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController: CommonApiController
    {
        private readonly CommonDtoService _service;
        public DataController(CommonDtoService service, ILogger<DataController> logger) : base(service.DataService, logger)
        {
            _service = service;
        }

        [HttpPost("collect")]
        public async Task<IActionResult> GetDataFromPost([FromBody] IDictionary<string, CommonQuery> queries)
        {
            try
            {
                return Ok(await _service.GetDtoCollectionsAsync(queries));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex);
                return BadRequest(badRequestDetails);
            }
        }

        [HttpGet("collect")]
        public async Task<IActionResult> GetData([FromQuery] IDictionary<string, string> paramList)
        {
            var queries = new Dictionary<string, CommonQuery>();
            foreach (var query in paramList)
            {
                try
                {
                    queries.Add(query.Key, JsonConvert.DeserializeObject<CommonQuery>(query.Value));
                }
                catch (Exception ex)
                {
                    var badRequestDetails = CreateProblemDetails(ex);
                    return BadRequest(badRequestDetails);
                }
            }
            return await GetDataFromPost(queries);
        }

        /// <summary>
        /// Gets enums with their values
        /// </summary>
        /// <param name="names">Enum names to get</param>
        /// <returns></returns>
        [HttpGet("enums")]
        public IActionResult GetEnums([FromQuery] string[] names)
        {
            try
            {
                var data = _service.GetEnumValues(names);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var problemDetails = CreateProblemDetails(ex);
                return BadRequest(problemDetails);
            }
        }

        /// <summary>
        /// Gets enum values by its name
        /// </summary>
        /// <param name="name">Enum name</param>
        /// <returns></returns>
        [HttpGet("enum")]
        public IActionResult GetEnum([FromQuery] string name)
        {
            try
            {
                var data = _service.GetEnumValues(name);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var problemDetails = CreateProblemDetails(ex);
                return BadRequest(problemDetails);
            }
        }
    }
}
