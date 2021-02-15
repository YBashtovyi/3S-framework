using System;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Data.Dto.Common;
using Core.Security;

namespace App.Data.Dto.Common
{
    [MainEntity(nameof(NotificationReceiver))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class NotificationReceiverListDto: BaseNotificationReceiverDto, IPagingCounted
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ReceiverCaption { get; set; }


        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid OrganizationId { get; set; }


        /// <inheritdoc/>
        public int TotalRecordCount { get; set; }
    }


    [MainEntity(nameof(NotificationReceiver))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class NotificationReceiverEditDto: BaseNotificationReceiverDto
    {
        public Guid OrganizationId { get; set; }
    }
}
