using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents information about base medical referral in E-Health
    /// </summary>
    public abstract class BaseEhealthMedicalReferralDto : BaseDto
    {
        /// <summary>
        /// Medical referral status
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid StatusId { get; set; }

        /// <summary>
        /// Status of referral processing.
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? ProcessingStatusInEhealthId { get; set; }

        /// <summary>
        /// Medical service program id in application
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? MedicalServiceProgramId { get; set; }

        /// <summary>
        /// Created medical referral date
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime RegDate { get; set; }

        /// <summary>
        /// Expiration medical referral date
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Medical referral priority id
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid PriorityId { get; set; }

        /// <summary>
        /// Medical referral category id
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicalReferralCategoryId { get; set; }

        /// <summary>
        /// Service catalog service id 
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? ServiceCatalogServiceId { get; set; }

        /// <summary>
        /// Service catalog group id
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? ServiceCatalogGroupId { get; set; }

        /// <summary>
        /// Performer speciality type id 
        /// </summary>
        /// <remarks>
        /// The field is required only for referrals with hospitalization or transfer_of_care category
        /// </remarks>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? PerformerTypeId { get; set; }

        /// <summary>
        /// Contains organization id in E-Health
        /// </summary>
        /// <remarks>
        /// The field is required only for referrals with hospitalization or transfer_of_care category
        /// </remarks>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? PerformerOrganizationId { get; set; }

        /// <summary>
        /// Contains department id in E-Health
        /// </summary>
        /// <remarks>
        /// The field is required only for referrals with transfer_of_care category
        /// </remarks>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? PerformerDepartmentId { get; set; }

        /// <summary>
        /// Recommended start time to receive the service 
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime? RecommendedReceivingStartTime { get; set; }

        /// <summary>
        /// Recommended end time to receive the service 
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime? RecommendedReceivingEndTime { get; set; }

        /// <summary>
        /// Notes in medical referral
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Description { get; set; }

        /// <summary>
        /// Instruction for the patient in medical referral
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string PatientInstruction { get; set; }

        /// <summary>
        /// group  number in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string GroupNumber { get; set; }

        /// <summary>
        /// Medical referral id in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? EhealthId { get; set; }
    }
}
