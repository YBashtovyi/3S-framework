using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Models.Common
{
    /// <summary>
    /// Represents basic notification entity 
    /// </summary>
    [Display(Name = "Сповіщення")]
    public abstract class BaseNotification : CoreEntity
    {
        /// <summary>
        /// Notification title
        /// </summary>
        [MaxLength(60)]
        public virtual string Title { get; set; }

        /// <summary>
        /// Notification content message
        /// </summary>
        [MaxLength(1000)]
        public virtual string Message { get; set; }

        /// <summary>
        /// URL when the user taps on the notification. 
        /// We can also specify a deep link for app
        /// </summary>
        [MaxLength(2000)]
        public virtual string Url { get; set; }

        /// <summary>
        /// Notification type id 
        /// </summary>
        public virtual Guid TypeId { get; set; }

        /// <summary>
        /// Notification transfer state id
        /// </summary>
        public virtual Guid StateId { get; set; }

        /// <summary>
        /// Schedule notifications for delivery at a time in the future.
        /// </summary>
        /// <remarks>
        /// Optional, required only for notification type =  scheduled
        /// </remarks>
        public virtual DateTime? ParticularTime { get; set; }

    }
}
