using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents information about outgoing medical referral in E-Health
    /// </summary>
    [Display(Name = "Виписані медичні направлення")]
    public abstract class BaseEhealthOutgoingMedicalReferral: BaseEhealthMedicalReferral
    {
        /// <summary>
        /// Organization id in application
        /// </summary>
        public virtual Guid OrganizationId { get; set; }

        /// <summary>
        /// Employee id in application
        /// </summary>
        public virtual Guid EmployeeId { get; set; }

        /// <summary>
        /// Ehealth employee id in application
        /// </summary>
        public virtual Guid EhealthEmployeeId { get; set; }

        /// <summary>
        /// Required, if canceling a referral
        /// </summary>
        public virtual Guid? CancellationStatusReasonId { get; set; }

        /// <summary>
        /// Is set when canceling a referral
        /// </summary>
        public virtual string CancellationExplanatoryLetter { get; set; }

        /// <summary>
        /// Ehealth field. Organization that completed a referral
        /// </summary>
        public virtual Guid? UsedByLegalEntityId { get; set; }

        /// <summary>
        /// Ehealth field. Employee that completed a referral
        /// </summary>
        public virtual Guid? UsedByEmployeeId { get; set; }

        /// <summary>
        /// Ehealth field. A concrete service that completes a referral
        /// </summary>
        public virtual Guid? UsedByProgramServiceId { get; set; }

        /// <summary>
        /// Ehealth field. When a referral was completed
        /// </summary>
        public virtual DateTime? CompletedOn { get; set; }

        /// <summary>
        /// Ehealth field. Entity id that completes a referral 
        /// </summary>
        public virtual Guid? CompletedWithId { get; set; }

        /// <summary>
        /// Ehealth field. Entity type id that completes a referral
        public virtual Guid? CompletedWithEntityTypeId { get; set; }
    }
}
