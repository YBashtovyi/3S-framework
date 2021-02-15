using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents group in service catalog
    /// </summary>
    [Display(Name = "Група в каталогі послуг")]
    public abstract class BaseEhealthServiceCatalogGroup: BaseEntity
    {
        /// <summary>
        /// Group code
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Indication that the group is active or inactive
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Indication that the group is allowed in the medical referral
        /// </summary>
        public virtual bool RequestAllowed { get; set; }

        /// <summary>
        /// Date of creation in E-Health
        /// </summary>
        public virtual DateTime InsertedAtInEhealth { get; set; }

        /// <summary>
        /// Date of updated in E-Health
        /// </summary>
        public virtual DateTime? UpdatedAtInEhealth { get; set; }

        /// <summary>
        /// Group id in E-Health
        /// </summary>
        public virtual Guid? EhealthId { get; set; }

        /// <summary>
        /// Parent service catalog group id.
        /// </summary>
        /// <remarks>
        /// If empty value, then group is main parent
        /// </remarks>
        public virtual Guid? ParentId { get; set; }
    }
}
