using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents group in service catalog
    /// </summary>
    public abstract class BaseEhealthServiceCatalogGroupDto: BaseDto
    {
        /// <summary>
        /// Group code
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string Code { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Indication that the group is active or inactive
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Indication that the group is allowed in the medical referral
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
        /// Group id in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? EhealthId { get; set; }

        /// <summary>
        /// Parent service catalog group id.
        /// </summary>
        /// <remarks>
        /// If empty, then group is main parent
        /// </remarks>
        [CaseFilter(CaseFilterOperation.Equals)] 
        public virtual Guid? ParentId { get; set; }
    }
}
