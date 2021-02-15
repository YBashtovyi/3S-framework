using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Data.Models.Common;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(NotificationReceiver))]
    [Table("CmnNotificationReceiver")]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class NotificationReceiver: BaseNotificationReceiver
    {
        /// <summary>
        /// Organization id in application <see cref="Models.Organization"/>
        /// </summary>
        public Guid OrganizationId { get; set; }


        #region properties: navigation

        public Employee Receiver { get; set; }


        public Notification Notification { get; set; }


        public Organization Organization { get; set; }
        #endregion
    }
}
