using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Data.Models.Common;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(Notification))]
    [Table("CmnNotification")]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class Notification : BaseNotification
    {
        /// <summary>
        /// Identifier of notification in OneSignal 
        /// </summary>
        public Guid? OneSignalId { get; set; }

        /// <summary>
        /// Error while sending notififcation
        /// </summary>
        [MaxLength(1000)]
        public string Error { get; set; }


        /// <summary>
        /// Date of creating notification in OneSignal
        /// </summary>
        public DateTime? CreateInOneSignalDate { get; set; }


        /// <summary>
        /// Organization id in application <see cref="Models.Organization"/>
        /// </summary>
        public Guid OrganizationId { get; set; }


        /// <summary>
        /// Number of notifications that were sent to Google/Apple servers.
        /// </summary>
        public int SuccessfulMessages { get; set; }


        /// <summary>
        /// Number of notifications that could not be delivered due to those devices being unsubscribed.
        /// </summary>
        public int FailedMessages { get; set; }


        /// <summary>
        /// Number of notifications that could not be delivered due to an error. 
        /// </summary>
        /// <remarks>
        /// You can find more information by viewing the notification in the dashboard.
        /// <see cref="https://app.onesignal.com/apps/57b3e8ac-1e3f-4877-812b-3b63f7f595ca/notifications"/>
        /// </remarks>
        public int Errored { get; set; }


        #region properties: navigation 

        public EnumRecord Type { get; set; }


        public EnumRecord State { get; set; }


        public Organization Organization { get; set; }
        
        #endregion
    }
}
