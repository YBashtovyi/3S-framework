using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents service in service catalog
    /// </summary>
    public abstract class BaseEhealthServiceCatalogServiceDto: BaseDto
    {
        /// <summary>
        /// Service code
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string Code { get; set; }

        /// <summary>
        /// Service name
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Medical referral category id
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicalReferralCategoryId { get; set; }

        /// <summary>
        /// Indication that the service is active or inactive
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Indication that the service is available in multiple groups
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsComposition { get; set; }

        /// <summary>
        /// Indication that the service is allowed in the medical referral
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool RequestAllowed { get; set; }

        /// <summary>
        /// Date of creation in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime InsertedAtInEhealth { get; set; }

        /// <summary>
        /// Date of updated in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime? UpdatedAtInEhealth { get; set; }

        /// <summary>
        /// Service id in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? EhealthId { get; set; }
    }
}
