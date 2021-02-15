using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents information about base medical referral in E-Health
    /// </summary>
    [Display(Name = "Медичні направлення E-Health")]
    public abstract class BaseEhealthMedicalReferral: BaseDocument
    {
        /// <summary>
        /// Patient card id in application
        /// </summary>
        public virtual Guid PatientCardId { get; set; }

        /// <summary>
        /// Medical referral status
        /// </summary>
        public virtual Guid StatusId { get; set; }

        /// <summary>
        /// Status of referral processing.
        /// </summary>
        public virtual Guid? ProcessingStatusInEhealthId { get; set; }

        /// <summary>
        /// Medical service program id in application
        /// </summary>
        public virtual Guid? MedicalServiceProgramId { get; set; }

        /// <summary>
        /// Expiration medical referral date
        /// </summary>
        public virtual DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Medical referral priority id
        /// </summary>
        public virtual Guid PriorityId { get; set; }

        /// <summary>
        /// Medical referral category id
        /// </summary>
        public virtual Guid MedicalReferralCategoryId { get; set; }

        /// <summary>
        /// Service catalog service id 
        /// </summary>
        public virtual Guid? ServiceCatalogServiceId { get; set; }

        /// <summary>
        /// Service catalog group id
        /// </summary>
        public virtual Guid? ServiceCatalogGroupId { get; set; }

        /// <summary>
        /// Performer speciality type id 
        /// </summary>
        /// <remarks>
        /// The field is required only for referrals with hospitalization or transfer_of_care category
        /// </remarks>
        public virtual Guid? PerformerTypeId { get; set; }

        /// <summary>
        /// Contains organization id in E-Health
        /// </summary>
        /// <remarks>
        /// The field is required only for referrals with hospitalization or transfer_of_care category
        /// </remarks>
        public virtual Guid? PerformerOrganizationId { get; set; }

        /// <summary>
        /// Contains department id in E-Health
        /// </summary>
        /// <remarks>
        /// The field is required only for referrals with transfer_of_care category
        /// </remarks>
        public virtual Guid? PerformerDepartmentId { get; set; }

        /// <summary>
        /// Recommended start time to receive the service 
        /// </summary>
        public virtual DateTime? RecommendedReceivingStartTime { get; set; }

        /// <summary>
        /// Recommended end time to receive the service 
        /// </summary>
        public virtual DateTime? RecommendedReceivingEndTime { get; set; }

        /// <summary>
        /// Instruction for the patient in medical referral
        /// </summary>
        [MaxLength(2000)]
        public virtual string PatientInstruction { get; set; }

        /// <summary>
        /// group  number in E-Health
        /// </summary>
        public virtual string GroupNumber { get; set; }

        /// <summary>
        /// Medical referral id in E-Health
        /// </summary>
        public virtual Guid? EhealthId { get; set; }
    }
}
