using System;
using System.ComponentModel.DataAnnotations;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents information about outgoing medical referral in E-Health
    /// </summary>
    [Display(Name = "Виписані медичні направлення")]
    public abstract class BaseEhealthOutgoingMedicalReferralDto: BaseEhealthMedicalReferralDto
    {
        /// <summary>
        /// Organization id in application
        /// </summary
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid OrganizationId { get; set; }

        /// <summary>
        /// Employee id in application
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid EmployeeId { get; set; }

        /// <summary>
        /// Ehealth employee id in application
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid EhealthEmployeeId { get; set; }

        /// <summary>
        /// Patient card id in application
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid PatientCardId { get; set; }
    }
}
