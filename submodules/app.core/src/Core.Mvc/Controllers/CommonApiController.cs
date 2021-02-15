﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Mvc.Helpers;
using Core.Base.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Core.Services.Data;
using Core.Base.Exceptions;
using Microsoft.Extensions.Logging;
using Core.Security;

namespace Core.Mvc.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class CommonApiController<TDto, TEntity>: CommonApiController<TDto, TDto, TDto, TEntity>
        where TDto : class, IGenericEntity<Guid>
        where TEntity : class, IGenericEntity<Guid>
    {
        public CommonApiController(ICommonDataService dataService,
            ILogger<CommonApiController> logger) : base(dataService, logger)
        {
        }
    }

    [Produces("application/json")]
    [ApiController]
    public class CommonApiController<TDetailDto, TEditDto, TEntity>: CommonApiController<TDetailDto, TEditDto, TEditDto, TEntity>
        where TDetailDto : class, IGenericEntity<Guid>
        where TEditDto : class, IGenericEntity<Guid>
        where TEntity : class, IGenericEntity<Guid>
    {
        public CommonApiController(ICommonDataService dataService,
            ILogger<CommonApiController> logger) : base(dataService, logger)
        {
        }
    }

    [Produces("application/json")]
    [ApiController]
    public class CommonApiController<TDetailDto, TEditDto, TListDto, TEntity>: CommonApiController
        where TDetailDto : class, IGenericEntity<Guid>
        where TEditDto : class, IGenericEntity<Guid>
        where TListDto : class, IGenericEntity<Guid>
        where TEntity : class, IGenericEntity<Guid>
    {
        #region Constructors
        public CommonApiController(ICommonDataService dataService,
            ILogger<CommonApiController> logger) : base(dataService, logger)
        {
        }

        #endregion Constructors

        #region Methods
        #region Actions

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
            return await List<TListDto>(paramList, null);
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
        [Route("ext")]
        public virtual async Task<IActionResult> GetItemsExt([FromQuery]IDictionary<string, string> paramList)
        {
            return await List<TDetailDto>(paramList, null);
        }

        /// <summary>
        /// Gets collection of items filtered and paginated by http query string.
        /// Method returns models with fields that are only part of base entity.
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
        [Route("model")]
        public virtual async Task<IActionResult> GetItemsClean([FromQuery]IDictionary<string, string> paramList)
        {
            return await List<TEditDto>(paramList, null);
        }

        /// <summary>
        /// Gets an item by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     /aa0d3321-3415-4878-bd7e-6ae8a3a5ec81
        /// </remarks>
        /// <param name="id">Id of an item</param>
        /// <returns>An item found by id</returns>
        /// <response code="200">If item exists</response>
        /// <response code="400">If user has no rights to get this item or there is an internal error</response> 
        /// <response code="404">If item not found</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public virtual async Task<IActionResult> GetItem(Guid id)
        {
            return await Details<TEditDto>(id, null);
        }

        /// <summary>
        /// Gets an item with extra fields by id. Such extra fields are not part of the item and are included for convenience
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     /aa0d3321-3415-4878-bd7e-6ae8a3a5ec81
        /// </remarks>
        /// <param name="id">Id of an item</param>
        /// <returns>An item found by id</returns>
        /// <response code="200">If item exists</response>
        /// <response code="400">If user has no rights to get this item or there is an internal error</response> 
        /// <response code="404">If item not found</response>
        [HttpGet("ext/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public virtual async Task<IActionResult> GetItemExt(Guid id)
        {
            return await Details<TDetailDto>(id, null);
        }

        /// <summary>
        /// Creates an item
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Body:
        ///     {
        ///         &quot;field1&quot;: 5,
        ///         &quot;field2&quot;: &quot;Smith&quot;
        ///     }
        /// </remarks>
        /// <param name="item">An item to be created</param>
        /// <returns>The created item</returns>
        /// <response code="201">If item created successfully</response>
        /// <response code="400">If user has no rights to create this item or there is an internal error</response> 
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public virtual async Task<IActionResult> PostItem(TEditDto item)
        {
            return await Create<TEditDto, TEntity>(item, nameof(GetItem), null);
        }

        /// <summary>
        /// Creates an item
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Body:
        ///     {
        ///         &quot;field1&quot;: 5,
        ///         &quot;field2&quot;: &quot;Smith&quot;
        ///     }
        /// </remarks>
        /// <param name="items">A collection of items to be created</param>
        /// <returns>The created item</returns>
        /// <response code="201">If items created successfully</response>
        /// <response code="400">If user has no rights to create this item or there is an internal error</response> 
        [HttpPost("batch")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public virtual async Task<IActionResult> PostCollection(IEnumerable<TEditDto> items)
        {
            return await CreateFromCollection<TEditDto, TEntity>(items, nameof(GetItems), null);
        }

        /// <summary>
        /// Updates an item
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     /aa0d3321-3415-4878-bd7e-6ae8a3a5ec81
        ///     Body:
        ///     {
        ///         &quot;id&quot;: &quot;aa0d3321-3415-4878-bd7e-6ae8a3a5ec81&quot;,
        ///         &quot;field1&quot;: 5,
        ///         &quot;field2&quot;: &quot;Smith&quot;
        ///     }
        /// </remarks>
        /// <param name="id">Id of an item</param>
        /// <param name="item">Item data</param>
        /// <returns>No content</returns>
        /// <response code="204">If item updated successfully</response>
        /// <response code="400">If user has no rights to update this item or there is an internal error</response> 
        /// <response code="404">If item not found</response>
        /// <response code="404">If there is a conflict in a body data. For example an id in the body is different from id in query string</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public virtual async Task<IActionResult> PutItem(Guid id, TEditDto item)
        {
            return await Update<TEditDto, TEntity>(id, item, null);
        }

        /// <summary>
        /// Updates an item with patch operation
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        ///     /aa0d3321-3415-4878-bd7e-6ae8a3a5ec81
        ///     Body:
        ///     [
	    ///         {&quot;op&quot; : &quot;replace&quot;, &quot;path&quot; : &quot;age&quot;, &quot;value&quot; : &quot;21&quot;},
	    ///         {&quot;op&quot; : &quot;replace&quot;, &quot;path&quot; : &quot;caption&quot;, &quot;value&quot; : &quot;Patched successfully&quot;}
        ///     ]
        /// </remarks>
        /// <param name="id">Id of an item</param>
        /// <param name="patchData">A set of operations formatted according to jsonpatch.com</param>
        /// <returns>No content</returns>
        /// <response code="204">If item patched successfully</response>
        /// <response code="400">If user has no rights to update this item or there is an internal error</response> 
        /// <response code="404">If item not found</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public virtual async Task<IActionResult> PatchItem(Guid id, JsonPatchDocument<TEditDto> patchData)
        {
            return await Patch<TEditDto, TEntity>(id, patchData, null);
        }

        /// <summary>
        /// Deletes an item by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     /aa0d3321-3415-4878-bd7e-6ae8a3a5ec81
        /// </remarks>
        /// <param name="id">Id of an item</param>
        /// <param name="softDeleting">Setting to true only marks an item as deleted. The item won't be accessible anymore from any method.</param>
        /// <returns>No content</returns>
        /// <response code="204">If item deleted successfully</response>
        /// <response code="400">If user has no rights to delete this item or there is an internal error</response> 
        /// <response code="404">If item not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public virtual async Task<IActionResult> DeleteItem(Guid id, bool softDeleting = true)
        {
            return await Delete<TEntity>(id, softDeleting, null);
        }
        #endregion Actions

        #endregion Methods
    }

    [Produces("application/json")]
    [ApiController]
    public class CommonApiController: ControllerBase
    {
        #region FieldsAndProperties
        protected ICommonDataService DataService { get; }
        private readonly ILogger _logger;
        #endregion

        #region Constructors
        public CommonApiController(ICommonDataService dataService,
            ILogger logger)
        {
            DataService = dataService;
            _logger = logger;
        }
        #endregion Constructors

        #region Methods

        [NonAction]
        public async Task<IActionResult> List<TDto>(IDictionary<string, string> paramList, Func<IDictionary<string, string>, Task<IEnumerable<TDto>>> listFunction) 
            where TDto : class, IGenericEntity<Guid>
        {
            IEnumerable<TDto> list;
            try
            {
                if (listFunction == null)
                {
                    var (pageSize, pageNumber, orderBy, otherParameters) = HttpQueryStringHelper.GetQueryParametersFromQueryParamList(paramList);
                    list = await DataService.GetDtoAsync<TDto>(orderBy, null, otherParameters, (pageNumber - 1) * pageSize, pageSize, 0, null, null);
                }
                else
                {
                    list = await listFunction(paramList);
                }
            }
            catch (NoRightsException nrex)
            {
                var badRequestDetails = CreateProblemDetails(nrex);
                return BadRequest(badRequestDetails);
            }
            catch (AppException aex)
            {
                var badRequestDetails = CreateProblemDetails(aex);
                return BadRequest(badRequestDetails);
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex);
                return BadRequest(badRequestDetails);
            }

            return Ok(list);
        }

        [NonAction]
        public async Task<IActionResult> Details<TDto>(Guid id, Func<Guid, Task<TDto>> detailFunction)
            where TDto : class, IGenericEntity<Guid>
        {
            TDto item = null;
            try
            {
                if (id != Guid.Empty)
                {
                    if (detailFunction == null)
                    {
                        item = (await DataService.GetDtoAsync<TDto>(x => x.Id == id)).SingleOrDefault();
                    }
                    else
                    {
                        item = await detailFunction(id);
                    }
                }
            }
            catch (NoRightsException nrex)
            {
                var badRequestDetails = CreateProblemDetails(nrex);
                return BadRequest(badRequestDetails);
            }
            catch (AppException aex)
            {
                var badRequestDetails = CreateProblemDetails(aex);
                return BadRequest(badRequestDetails);
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex);
                return BadRequest(badRequestDetails);
            }

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [NonAction]
        public async Task<IActionResult> Create<TDto, TEntity>(TDto item, string createdAt, Func<TDto, Task<Dictionary<string, string>>> createFunction) 
            where TDto : class, IGenericEntity<Guid> 
            where TEntity : class, IGenericEntity<Guid>
        {
            try
            {
                if (createFunction == null)
                {
                    item.Id = DataService.AddDto<TEntity>(item, false);
                    await DataService.SaveChangesAsync();
                }
                else
                {
                    var errors = await createFunction(item);
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    if (errors.Any())
                    {
                        return BadRequest(new ValidationProblemDetails(ModelState));
                    }
                }
            }
            catch (NoRightsException nrex)
            {
                var badRequestDetails = CreateProblemDetails(nrex);
                return BadRequest(badRequestDetails);
            }
            catch (AppException aex)
            {
                var badRequestDetails = CreateProblemDetails(aex);
                return BadRequest(badRequestDetails);
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex);
                return BadRequest(badRequestDetails);
            }

            return CreatedAtAction(createdAt, new { id = item.Id }, item);
        }

        [NonAction]
        public async Task<IActionResult> CreateFromCollection<TDto, TEntity>(IEnumerable<TDto> dtoCollection, string createdAt, Func<IEnumerable<TDto>, Task<Dictionary<string, string>>> createFunction)
            where TDto : class, IGenericEntity<Guid> 
            where TEntity : class, IGenericEntity<Guid>
        {
            try
            {
                if (createFunction == null)
                {
                    foreach (var item in dtoCollection)
                    {
                        item.Id = DataService.AddDto<TEntity>(item, false);
                    }
                    await DataService.SaveChangesAsync();
                }
                else
                {
                    var errors = await createFunction(dtoCollection);
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    if (errors.Any())
                    {
                        return BadRequest(new ValidationProblemDetails(ModelState));
                    }
                }
            }
            catch (NoRightsException nrex)
            {
                var badRequestDetails = CreateProblemDetails(nrex);
                return BadRequest(badRequestDetails);
            }
            catch (AppException aex)
            {
                var badRequestDetails = CreateProblemDetails(aex);
                return BadRequest(badRequestDetails);
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex);
                return BadRequest(badRequestDetails);
            }

            return CreatedAtAction(createdAt, dtoCollection);
        }

        [NonAction]
        public async Task<IActionResult> Update<TDto, TEntity>(Guid id, TDto item, Func<Guid, TDto, Task<Dictionary<string, string>>> editFunction) 
            where TDto : class, IGenericEntity<Guid>
            where TEntity : class, IGenericEntity<Guid>
        {
            if (id != item.Id)
            {
                var conflictDetails = new ProblemDetails { Status = 409, Title = "Request data conflict", Detail = $"id {id} does not equal to item id {item.Id}" };
                return BadRequest(conflictDetails);
            }

            try
            {
                if (editFunction == null)
                {
                    if (DataService.AddDto<TEntity>(item, isUpdating: true) == Guid.Empty)
                    {
                        return NotFound();
                    }

                    await DataService.SaveChangesAsync();
                }
                else
                {
                    var errors = await editFunction(id, item);
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    if (errors.Any())
                    {
                        return BadRequest(new ValidationProblemDetails(ModelState));
                    }
                }
            }
            catch (NoRightsException nrex)
            {
                var badRequestDetails = CreateProblemDetails(nrex);
                return BadRequest(badRequestDetails);
            }
            catch (AppException aex)
            {
                var badRequestDetails = CreateProblemDetails(aex);
                return BadRequest(badRequestDetails);
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex);
                return BadRequest(badRequestDetails);
            }

            return NoContent();
        }

        [NonAction]
        public async Task<IActionResult> Patch<TDto, TEntity>(Guid id, JsonPatchDocument<TDto> patchData, Func<Guid, JsonPatchDocument<TDto>, Task<Dictionary<string, string>>> patchFunction) 
            where TDto : class, IGenericEntity<Guid>
            where TEntity : class, IGenericEntity<Guid>
        {
            try
            {
                if (patchFunction == null)
                {
                    var item = (await DataService.GetDtoAsync<TDto>(x => x.Id == id)).SingleOrDefault();
                    if (item == null)
                    {
                        return NotFound();
                    }
                    patchData.ApplyTo(item);
                    item.Id = id;

                    if (DataService.AddDto<TEntity>(item, isUpdating: true) == Guid.Empty)
                    {
                        return NotFound();
                    }

                    await DataService.SaveChangesAsync();
                }
                else
                {
                    var errors = await patchFunction(id, patchData);
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    if (errors.Any())
                    {
                        return BadRequest(new ValidationProblemDetails(ModelState));
                    }
                }
            }
            catch (NoRightsException nrex)
            {
                var badRequestDetails = CreateProblemDetails(nrex);
                return BadRequest(badRequestDetails);
            }
            catch (AppException aex)
            {
                var badRequestDetails = CreateProblemDetails(aex);
                return BadRequest(badRequestDetails);
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex);
                return BadRequest(badRequestDetails);
            }

            return NoContent();
        }

        [NonAction]
        public async Task<IActionResult> Delete<TEntity>(Guid id, bool softDeleting, Func<Guid, bool, Task<TEntity>> deleteAction)
            where TEntity : class, IGenericEntity<Guid>
        {
            TEntity entity;
            try
            {
                if (deleteAction == null)
                {
                    entity = DataService.Remove<TEntity>(id, softDeleting);

                    if (entity == null)
                    {
                        return NotFound();
                    }

                    await DataService.SaveChangesAsync();
                }
                else
                {
                    entity = await deleteAction(id, softDeleting);
                    if (entity == null)
                    {
                        return NotFound();
                    }
                }
            }
            catch (NoRightsException nrex)
            {
                var badRequestDetails = CreateProblemDetails(nrex);
                return BadRequest(badRequestDetails);
            }
            catch (AppException aex)
            {
                var badRequestDetails = CreateProblemDetails(aex);
                return BadRequest(badRequestDetails);
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex);
                return BadRequest(badRequestDetails);
            }

            return NoContent();
        }

        private ProblemDetails CreateProblemDetails(AppException ex, int status = 400, string message = null)
        {
            if (_logger != null)
            {
                _logger.LogError(new EventId(status), ex, "Error occured in controller. See exception details. Error message: {Message}",
                    ex.Message);
            }
            var badRequestDetails = new ProblemDetails { Status = status, Title = ex.Title ?? "Bad request", Detail = message ?? ex.Message };
            return badRequestDetails;
        }

        private ProblemDetails CreateProblemDetails(NoRightsException ex)
        {
            if (_logger != null)
            {
                _logger.LogError(new EventId(403), ex, "Access error occured. Error message: {Message}",
                    ex.Message);
            }
            var badRequestDetails = new ProblemDetails { Status = 403, Title = ex.Title ?? "Access denied", Detail = ex.Message };
            return badRequestDetails;
        }

        private ProblemDetails CreateProblemDetails(Exception ex, int status = 400, string details = "Error occurred (details hidden)", string title = "Bad request")
        {
            if (_logger != null)
            {
                _logger.LogError(new EventId(status), ex, "Error occured in controller. See exception details.");
            }
            var badRequestDetails = new ProblemDetails { Status = status, Title = title, Detail = details };
            return badRequestDetails;
        }

        #endregion Methods
    }
}
