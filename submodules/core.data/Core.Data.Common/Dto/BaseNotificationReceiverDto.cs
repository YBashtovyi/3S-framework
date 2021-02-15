using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Common.Dto
{
    public abstract class BaseNotificationReceiverDto : BaseDto
    {
        /// <summary>
        /// Notification id
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid NotificationId { get; set; }

        /// <summary>
        /// Recevier id
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ReceiverId { get; set; }
    }
}
