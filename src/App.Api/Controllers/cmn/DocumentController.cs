using Microsoft.AspNetCore.Mvc;
using App.Data.Dto.Common;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace App.WebAPI.Controllers.cmn
{
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentController : CommonApiController<DocumentListDto, DocumentListDto, DocumentListDto, Document>
    {
        public DocumentController(ICommonDataService dataService, ILogger<DocumentController> logger) : base(dataService, logger)
        {
        }
    }
}
