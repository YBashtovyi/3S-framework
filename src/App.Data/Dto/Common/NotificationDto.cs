using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Data.Dto.Common;
using Core.Security;

namespace App.Data.Dto.Common
{
    [MainEntity(nameof(Notification))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class NotificationByAuthorListDto: BaseNotificationDto, IPagingCounted
    {

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime CreatedAt { get; set; }


        [CaseFilter(CaseFilterOperation.Contains)]
        public string TypeCaption { get; set; }


        [CaseFilter(CaseFilterOperation.Contains)]
        public string  StateCaption { get; set; }


        [CaseFilter(CaseFilterOperation.Equals)]
        public string StateCode { get; set; }


        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid AuthorId { get; set; }


        [CaseFilter(CaseFilterOperation.Contains)]
        public string AuthorCaption { get; set; }


        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime? CreateInOneSignalDate { get; set; }


        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid OrganizationId { get; set; }


        [CaseFilter(CaseFilterOperation.Contains)]
        public string OrganizationCaption { get; set; }

        /// <inheritdoc/>
        public int TotalRecordCount { get; set; }
    }


    [MainEntity(nameof(Notification))]
    //[RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class NotificationByReceiverListDto: BaseNotificationDto, IPagingCounted
    {

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime CreatedAt { get; set; }


        [CaseFilter(CaseFilterOperation.Contains)]
        public string TypeCaption { get; set; }


        [CaseFilter(CaseFilterOperation.Contains)]
        public string StateCaption { get; set; }


        [CaseFilter(CaseFilterOperation.Equals)]
        public string StateCode { get; set; }


        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid AuthorId { get; set; }


        [CaseFilter(CaseFilterOperation.Contains)]
        public string AuthorCaption { get; set; }


        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime? CreateInOneSignalDate { get; set; }


        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid OrganizationId { get; set; }


        [CaseFilter(CaseFilterOperation.Contains)]
        public string OrganizationCaption { get; set; }


        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid? ReceiverId { get; set; }

        /// <inheritdoc/>
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(Notification))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class NotificationEditDto: BaseNotificationDto
    {        
        /// <summary>
        /// Identifier of notification in OneSignal
        /// </summary>
        public Guid? OneSignalId { get; set; }


        /// <summary>
        /// Error while sending notififcation
        /// </summary>
        public string Error { get; set; }


        /// <summary>
        /// Date of creating notification in OneSignal
        /// </summary>
        public DateTime? CreateInOneSignalDate { get; set; }


        public Guid OrganizationId { get; set; }


        [NotMapped]
        public IEnumerable<NotificationReceiverEditDto> Receivers = new List<NotificationReceiverEditDto>();
    }

    [MainEntity(nameof(Notification))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class NotificationDetailDto: BaseNotificationDto
    {
        public DateTime CreatedAt { get; set; }


        public string TypeCaption { get; set; }


        public string StateCode { get; set; }


        public string StateCaption { get; set; }


        public Guid AuthorId { get; set; }


        public string AuthorCaption { get; set; }


        /// <summary>
        /// Error while sending notififcation
        /// </summary>
        public string Error { get; set; }


        /// <summary>
        /// Date of creating notification in OneSignal
        /// </summary>
        public DateTime? CreateInOneSignalDate { get; set; }

        public Guid OrganizationId { get; set; }


        [NotMapped]
        public IEnumerable<NotificationReceiverListDto> Receivers = new List<NotificationReceiverListDto>();
    }
}
