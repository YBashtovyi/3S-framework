using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Data.Dto.Common.NotMapped;
using Newtonsoft.Json.Linq;

namespace App.Business.Services.ApplicationServices
{
    /// <summary>
    /// Definition of methods for working with the OneSignal library
    /// </summary>
    public interface IOneSignalService
    {
        /// <summary>
        /// View the details of a single notification and outcomes associated with it
        /// </summary>
        /// <param name="id">notification id</param>
        /// <returns></returns>
        Task<JObject> GetNotificationByIdAsync(Guid id);

        /// <summary>
        /// Sends notifications to your users
        /// </summary>
        /// <remarks>
        /// The Create Notification method is used when you want your server 
        /// to programmatically send notifications or emails to a segment or individual users.
        /// </remarks>
        /// <param name="title">Message title</param>
        /// <param name="content">Message body</param>
        /// <param name="users">users. If users = null, send notification to all subscribed users</param>
        /// <returns></returns>
        Task<NotificationCreateResponseDto> CreateNotificationAsync(string title, string content, IEnumerable<Guid> users = null);

        /// <summary>
        /// Sends notifications to your users
        /// </summary>
        /// <remarks>
        /// The Create Notification method is used when you want your server 
        /// to programmatically send notifications or emails to a segment or individual users.
        /// </remarks>
        /// <param name="title">Message title</param>
        /// <param name="content">Message body</param>
        /// <param name="url">Message url</param>
        /// <param name="users">users. If users = null, send notification to all subscribed users</param>
        /// <returns></returns>
        Task<NotificationCreateResponseDto> CreateNotificationAsync(string title, string content, string url, IEnumerable<Guid> users = null);

        /// <summary>
        /// Sends notifications to your users
        /// </summary>
        /// <remarks>
        /// The Create Notification method is used when you want your server 
        /// to programmatically send notifications or emails to a segment or individual users.
        /// </remarks>
        /// <param name="title">Message title</param>
        /// <param name="content">Message body</param>
        /// <param name="particularTime">Message sent after time</param>
        /// <param name="users">users. If users = null, send notification to all subscribed users</param>
        /// <returns></returns>
        Task<NotificationCreateResponseDto> CreateNotificationAsync(string title, string content, DateTime particularTime, IEnumerable<Guid> users = null);

        /// <summary>
        /// Sends notifications to your users
        /// </summary>
        /// <remarks>
        /// The Create Notification method is used when you want your server 
        /// to programmatically send notifications or emails to a segment or individual users.
        /// </remarks>
        /// <param name="title">Message title</param>
        /// <param name="content">Message body</param>
        /// <param name="url">Message url</param>
        /// <param name="particularTime">Message sent after time</param>
        /// <param name="users">users. If users = null, send notification to all subscribed users</param>
        /// <returns></returns>
        Task<NotificationCreateResponseDto> CreateNotificationAsync(string title, string content, string url, DateTime particularTime, IEnumerable<Guid> users = null);

    }
}
