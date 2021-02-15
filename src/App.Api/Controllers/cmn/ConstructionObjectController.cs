using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Business;
using App.Business.Services.ConstructionObjectServices;
using App.Business.ViewModels;
using App.Data.Dto.Cdn;
using Microsoft.AspNetCore.Mvc;
using App.Data.Dto.Common;
using App.Data.Dto.Common.NotMapped;
using App.Data.Dto.Prj;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static App.Business.Helpers.ControllerHelper;

namespace App.WebAPI.Controllers.cmn
{
    [Route("api/[controller]")]
    [Authorize]
    public class ConstructionObjectController : CommonApiController<ConstructionObjectDetailsDto, ConstructionObjectEditDto, ConstructionObjectListDto, ConstructionObject>
    {
        private readonly ILogger<ConstructionObjectController> _logger;
        private readonly ConstructionObjectService _constructionService;

        public ConstructionObjectController(ICommonDataService dataService, ILogger<ConstructionObjectController> logger, ConstructionObjectService constructionService) : base(dataService, logger)
        {
            _logger = logger;
            _constructionService = constructionService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("edit-page/{id}")]
        public async Task<IActionResult> GetEditPage(Guid id)
        {
            return await Details<ConstructionObjectEditDto>(id, null);
        }

        public override async Task<IActionResult> GetItemExt(Guid id)
        {
            var constObject = await DataService.SingleOrDefaultAsync<ConstructionObjectDetailsDto>(p => p.Id == id);
            if (constObject == null)
            {
                return Ok();
            }

            if (string.IsNullOrEmpty(constObject.AtuCoordinates))
            {
                return Ok(constObject);
            }

            constObject.AtuCoordinateList = JsonConvert.DeserializeObject<MapCoordinate>(constObject.AtuCoordinates);

            return Ok(constObject);
        }

        /// <summary>
        /// Get a list of projects that use this object
        /// </summary>
        /// <param name="id">ConstructionObjectId</param>
        /// <param name="paramList">Filters</param>
        /// <returns></returns>
        [HttpGet("get-project-construction-object/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProjectConstructionObject(Guid id, [FromQuery] IDictionary<string, string> paramList)
        {
            paramList.Add("constructionObjectId", id.ToString());
            return await List<ProjectConstructionObjectListDto>(paramList, null);
        }

        public override async Task<IActionResult> DeleteItem(Guid id, bool softDeleting = true)
        {
            try
            {
                // TODO: Перенести в сервис и добавить удаление связей extendedProperty
                var constObject = await DataService.SingleOrDefaultAsync<ConstructionObjectDetailsDto>(p => p.Id == id);
                if (constObject == null)
                {
                    return Ok();
                }

                var prjObject = await DataService.GetEntityAsync<ProjectConstructionObject>(p => p.ConstructionObjectId == id, false);
                if (prjObject.Any())
                {
                    //throw new AppException("Видалення неможливе, оскільки в Системі існують посилання на даний запис!");
                    var badRequestDetails = CreateProblemDetails(new AppException("Видалення неможливе, оскільки в Системі існують посилання на даний запис!"), _logger, nameof(PostItem));
                    return BadRequest(badRequestDetails);
                }

                DataService.Remove<ConstructionObject>(id, softDeleting);
                await DataService.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Changes the status of an object
        /// </summary>
        /// <param name="id">ConstructionObjectId</param>
        /// <param name="status">The status to which you want to change</param>
        /// <returns></returns>
        [HttpPut("change-object-status/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> ChangeObjectStatus(Guid id, string status)
        {
            try
            {
                await _constructionService.ChangeObjectStatus(id, status);
                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(ChangeObjectStatus));
                return BadRequest(badRequestDetails);
            }
        }

        #region ATU Coordinate

        /// <summary>
        /// Add coordinate to object
        /// </summary>
        /// <param name="objId">ConstructionObjectId</param>
        /// <param name="coordinate">Coordinate object</param>
        /// <returns>Returns a list of object coordinates</returns>
        [HttpPost("add-atu-coordinate/{objId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCoordinate(Guid objId, MapCoordinate coordinate)
        {
            try
            {
                return Ok(await _constructionService.AddCoordinate(objId, coordinate));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(AddCoordinate));
                return BadRequest(badRequestDetails);
            }
        }

        [HttpGet("get-atu-coordinate/{objId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCoordinates(Guid objId)
        {
            try
            {
                return Ok(await _constructionService.GetCoordinates(objId));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(GetCoordinates));
                return BadRequest(badRequestDetails);
            }
        }

        #endregion

        #region ExtendedProperty

        [HttpGet("get-extended-property/{id}")]
        public async Task<IActionResult> GetConstructionObjectExtendedProperty(Guid id, [FromQuery] IDictionary<string, string> paramList)
        {
            paramList.Add("ConstructionObjectId", id.ToString());
            return await List<ConstructionObjectExtendedPropertyListDto>(paramList, null);
        }

        [HttpPost("add-extended-property/{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddExtendedProperty(Guid id, ConstructionObjectExtendedPropertyViewModel model)
        {
            try
            {
                await _constructionService.AddConstructionObjectExtendedProperty(id, model);
                return Ok();
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(AddExtendedProperty));
                return BadRequest(badRequestDetails);
            }
        }

        [HttpDelete("delete-extended-property/{id}")]
        public async Task<IActionResult> DeleteExtendedProperty(Guid id)
        {
            return await Delete<ConstructionObjectExtendedProperty>(id, false, null);
        }

        #endregion
    }
}
