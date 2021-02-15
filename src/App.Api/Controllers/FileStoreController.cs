using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using App.Business.Services.ApiControllerServices;
using App.Data.Dto.System;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Mvc.Controllers;
using Core.Security;
using Core.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", MediaTypeNames.Text.Xml)]
    public class FileStoreController: CommonApiController
    {
        private readonly FileStoreControllerService _controllerService;
        private readonly ILogger<FileStoreController> _logger;

        public FileStoreController(FileStoreControllerService controllerService, ILogger<FileStoreController> logger)
            : base(controllerService.DataService, logger)
        {
            _controllerService = controllerService;
            _logger = logger;
        }

        /// <summary>
        /// Uploades file to server from form
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="201">If item created successfully</response>
        /// <response code="400">If user has no rights to create this item or there is an internal error</response> 
        [HttpPost("form-upload"), DisableRequestSizeLimit]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(415)]
        public async Task<IActionResult> UploadFromForm([FromForm]Guid entityId, [FromForm]string entityName, [FromForm]Guid? documentTypeId, [FromForm]string description, [FromForm]string typeOfAttachedFile)
        {
            IEnumerable<FileStoreDto> fileMetadata;
            try
            {
                var file = Request.Form.Files[0];
                fileMetadata = await _controllerService.SaveFileAsync(file, entityId, entityName, documentTypeId, null, description, typeOfAttachedFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while uploading file.");
                var badRequestDetails = new ProblemDetails { Status = 400, Title = "Bad request", Detail = $"{ex.Message}" };
                return BadRequest(badRequestDetails);
            }

            var createdFile = fileMetadata?.FirstOrDefault();
            if (createdFile == null)
            {
                var badRequestDetails = new ProblemDetails { Status = 400, Title = "File was not saved", Detail = "File was not saved. Unknown error occured." };
                return BadRequest(badRequestDetails);
            }

            return CreatedAtAction(nameof(Download), new { id = createdFile.Id }, createdFile);
        }


        /// <summary>
        /// Uploads files to server
        /// </summary>
        /// <param name="fileDto">file metadata with file data itself as fileData field</param>
        /// <returns></returns>
        [HttpPost("upload"), DisableRequestSizeLimit]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(415)]
        public async Task<IActionResult> Upload(FileEmbeddedDto fileDto)
        {
            IEnumerable<FileStoreDto> fileMetadata;
            try
            {
                fileMetadata = await _controllerService.SaveFilesAsync(fileDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while uploading files.");
                var badRequestDetails = new ProblemDetails { Status = 400, Title = "Bad request", Detail = $"{ex.Message}" };
                return BadRequest(badRequestDetails);
            }

            var createdFile = fileMetadata?.FirstOrDefault();
            if (createdFile == null)
            {
                var badRequestDetails = new ProblemDetails { Status = 400, Title = "File was not saved", Detail = "File was not saved. Unknown error occured." };
                return BadRequest(badRequestDetails);
            }

            return CreatedAtAction(nameof(Download), new { id = createdFile.Id }, createdFile);
        }

        /// <summary>
        /// Downloads file from server
        /// </summary>
        /// <param name="id">file id, that should be found</param>
        /// <returns></returns>
        [HttpGet("download/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(415)]
        public async Task<IActionResult> Download(Guid id)
        {
            (byte[] data, string fileName, string contentType) fileData;
            try
            {
                fileData = await _controllerService.GetFileAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while downloading files.");
                var badRequestDetails = new ProblemDetails { Status = 400, Title = "Bad request", Detail = $"{ex.Message}" };
                return BadRequest(badRequestDetails);
            }
            
            if (fileData.data == null || fileData.data.Length == 0)
            {
                return NotFound();
            }

            return File(fileData.data, fileData.contentType, fileData.fileName);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(415)]
        public async Task<IActionResult> GetItems([FromQuery] IDictionary<string, string> paramList)
        {
            return await List<FileStoreDto>(paramList, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await Delete<FileStore>(id, true, null);
        }
    }
}
