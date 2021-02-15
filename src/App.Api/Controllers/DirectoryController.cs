using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class DirectoryController<TDetailDto, TDto>: CommonApiController
        where TDetailDto : class, IGenericEntity<Guid>
        where TDto : class, IGenericEntity<Guid>
    {
        public DirectoryController(ICommonDataService dataService, ILogger<DirectoryController<TDetailDto, TDto>> logger) : base(dataService, logger)
        {
        }

        /// <summary>
        /// Gets collection of items filtered and paginated by http query string
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     ?orderBy=caption&amp;age=5
        /// </remarks>
        /// <param name="paramList">Query string. Supports next parameters
        /// pageSize - int, use for pagination
        /// pageNumber - int, use for pagination, starts from 1
        /// orderBy - field to sort
        /// [model field name] - for filtering data by this field. 
        /// Usually string values filtered by part &quot;like&quot; expression, Guid by &quot;equals&quot; expression.
        /// Periods filtered by &quot;overlaps&quot; expression. For example, if you have period with fields startDate = &quot;2019-03-10T14:58:05&quot; and endDate = &quot;2019-03-16T00:00:00&quot;
        /// then you should set both fileds in a query filter as a required period. 
        /// For example setting in a query string ?startDate=2019-03-01T00:00:00&amp;endDate=2019-03-15T05:00:00 you will get all records,
        /// where required period overlaps your filter.
        /// Date fields and numeric fields usually filtered by range. For example, if model has a date field registrationDate, then you can filter record with next query:
        /// ?registrationDate_From=2019-03-10T00:00:00&amp;registrationDate_To=2019-03-18T05:00:00
        /// IMPORTANT: every field can be filtered in a way (range, equals etc), configured on server and cannot be changed. Some fields cannot be filtered at all
        /// </param>
        /// <returns>A filtered list of items</returns>
        /// <response code="200">If there is no any error</response>
        /// <response code="400">If user has no rights to get this collection or there is an error in query filter string</response> 
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public virtual async Task<IActionResult> GetItems([FromQuery]IDictionary<string, string> paramList)
        {
            return await List<TDto>(paramList, null);
        }

        /// <summary>
        /// Gets collection of items filtered and paginated by http query string.
        /// Method returns models with extra fields that are not part of base entity. For example caption of related entity joined by Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     ?orderBy=caption&amp;age=5
        /// </remarks>
        /// <param name="paramList">Query string. Supports next parameters
        /// pageSize - int, use for pagination
        /// pageNumber - int, use for pagination, starts from 1
        /// orderBy - field to sort
        /// [model field name] - for filtering data by this field. 
        /// Usually string values filtered by part &quot;like&quot; expression, Guid by &quot;equals&quot; expression.
        /// Periods filtered by &quot;overlaps&quot; expression. For example, if you have period with fields startDate = &quot;2019-03-10T14:58:05&quot; and endDate = &quot;2019-03-16T00:00:00&quot;
        /// then you should set both fileds in filter as a required period. 
        /// For example setting in a query string ?startDate=2019-03-01T00:00:00&amp;endDate=2019-03-15T05:00:00 you will get all records,
        /// where required period overlaps your filter.
        /// Date fields and numeric fields usually filtered by range. For example, if model has a date field, for example registrationDate, then you can filter record with next query:
        /// ?registrationDate_From=2019-03-10T00:00:00&amp;registrationDate_To=2019-03-18T05:00:00
        /// IMPORTANT: every field can be filtered in a way (range, equals etc), configured on server and cannot be changed. Some fields cannot be filtered at all
        /// </param>
        /// <returns>A filtered list of items</returns>
        /// <response code="200">If there is no any error</response>
        /// <response code="400">If user has no rights to get this collection or there is an error in query filter string</response> 
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("Ext")]
        public virtual async Task<IActionResult> GetItemsExt([FromQuery]IDictionary<string, string> paramList)
        {
            return await List<TDetailDto>(paramList, null);
        }
    }
}
