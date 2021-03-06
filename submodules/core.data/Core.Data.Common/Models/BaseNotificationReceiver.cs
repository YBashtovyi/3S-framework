﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Common.Models
{
    /// <summary>
    /// Relation between notification and receiver of this notification
    /// </summary>
    [Display(Name = "Отримувач у сповіщенні")]
    public abstract class BaseNotificationReceiver: BaseEntity
    {
        /// <summary>
        /// Notification id
        /// </summary>
        public virtual Guid NotificationId { get; set; }

        /// <summary>
        /// Recevier id
        /// </summary>
        public virtual Guid ReceiverId { get; set; }
    }
}
