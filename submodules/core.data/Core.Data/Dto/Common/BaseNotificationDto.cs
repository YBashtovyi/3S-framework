using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Dto.Common
{
    public abstract class BaseNotificationDto: BaseDto
    {
        /// <summary>
        /// Notification title
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Title { get; set; }

        /// <summary>
        /// Notification content message
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Message { get; set; }

        /// <summary>
        /// URL when the user taps on the notification. 
        /// We can also specify a deep link for app
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Url { get; set; }

        /// <summary>
        /// Notification type id 
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid TypeId { get; set; }

        /// <summary>
        /// Notification transfer state id
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid StateId { get; set; }

        /// <summary>
        /// Schedule notifications for delivery at a time in the future.
        /// </summary>
        /// <remarks>
        /// Optional, required only for notification type =  scheduled
        /// </remarks>
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime? ParticularTime { get; set; }
    }
}
