using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Business.Services.ApiControllerServices;
using App.Data.Dto.Common;
using App.Data.Helpers;
using App.Data.Models;
using Core.Mvc.Controllers;
using Core.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static App.Business.Helpers.ControllerHelper;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController: CommonApiController<NotificationDetailDto, NotificationEditDto, Notification>
    {
        private readonly NotificationService _service;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(NotificationService service, ILogger<NotificationController> logger) : base(service.DataService, logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Gets main data and related entities for create/edit mode 
        /// </summary>
        /// <param name="notificationId">Guid - object id in application, if exists <see cref="Notification"/></param>
        /// <returns></returns>
        [HttpGet("edit-page-data/{notificationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNotificationEditData(Guid notificationId)
        {
            try
            {
                return Ok(await _service.GetNotificationEditDataAsync(notificationId));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(GetNotificationEditData));
                return BadRequest(badRequestDetails);
            }
        }


        /// <summary>
        /// Gets notifications by author
        /// </summary>
        /// <param name="paramList"></param>
        /// <returns></returns>
        [HttpGet("items-by-author")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNotificationByAuthorList([FromQuery] IDictionary<string, string> paramList)
        {
            try
            {
                return Ok(await _service.GetNotificationByAuthorListAsync(paramList));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(GetNotificationByAuthorList));
                return BadRequest(badRequestDetails);
            }
        }


        /// <summary>
        /// Gets notifications by receiver
        /// </summary>
        /// <param name="paramList"></param>
        /// <returns></returns>
        [HttpGet("items-by-receiver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNotificationByReceiverList([FromQuery] IDictionary<string, string> paramList)
        {
            try
            {
                return Ok(await _service.GetNotificationByReceiverListAsync(paramList));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(GetNotificationByReceiverList));
                return BadRequest(badRequestDetails);
            }
        }

        /// <summary>
        /// Create notification in application
        /// </summary>
        /// <param name="item">notification data</param>
        /// <returns></returns>
        [OperationRight(nameof(ApplicationOperations.CreateNotification))]
        public override Task<IActionResult> PostItem(NotificationEditDto item)
        {
            return Create<NotificationEditDto, Notification>(item, null, _service.CreateNotificationAsync);
        }

        /// <summary>
        /// Update notification in application
        /// </summary>
        /// <param name="id">notification identifier <see cref="Notification"/></param>
        /// <param name="item">notification data</param>
        /// <returns></returns>
        [OperationRight(nameof(ApplicationOperations.UpdateNotification))]
        public override Task<IActionResult> PutItem(Guid id, NotificationEditDto item)
        {
            return Update<NotificationEditDto, Notification>(id, item, _service.UpdateNotificationAsync);
        }

        /// <summary>
        /// Get notification details
        /// </summary>
        /// <param name="id">notification identifier <see cref="Notification"/></param>
        /// <returns></returns>
        public override Task<IActionResult> GetItemExt(Guid id)
        {
            return Details(id, _service.GetNotificationDetailsByIdAsync);
        }

        #region OneSignal
        /// <summary>
        /// Gets data about notification from OneSignal
        /// </summary>
        /// <param name="notificationId">Guid - object id in application, if exists <see cref="Notification"/></param>
        /// <returns></returns>
        [HttpGet("get-from-oneSignal/{notificationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNotificationFromOneSignalById(Guid notificationId)
        {
            try
            {
                return Ok(await _service.GetNotificationFromOneSignalByIdAsync(notificationId));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(GetNotificationFromOneSignalById));
                return BadRequest(badRequestDetails);
            }
        }

        /// <summary>
        /// Create notification in OneSignal
        /// </summary>
        /// <param name="notificationId">Guid - object id in application, if exists <see cref="Notification"/></param>
        /// <returns></returns>
        [HttpPost("create-in-oneSignal/{notificationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [OperationRight(nameof(ApplicationOperations.SendNotification))]
        public async Task<IActionResult> CreateNotificationInOneSignal(Guid notificationId)
        {
            try
            {
                return Ok(await _service.CreateNotificationInOneSignalAsync(notificationId));
            }
            catch (Exception ex)
            {
                var badRequestDetails = CreateProblemDetails(ex, _logger, nameof(CreateNotificationInOneSignal));
                return BadRequest(badRequestDetails);
            }
        }
        #endregion
    }
}
