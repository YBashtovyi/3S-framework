using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents service in service catalog
    /// </summary>
    [Display(Name = "Послуга в каталогі послуг")]
    public abstract class BaseEhealthServiceCatalogService: BaseEntity
    {
        /// <summary>
        /// Service code
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Service name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Medical referral category id
        /// </summary>
        public virtual Guid MedicalReferralCategoryId { get; set; }

        /// <summary>
        /// Indication that the service is active or inactive
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Indication that the service is available in multiple groups
        /// </summary>
        public virtual bool IsComposition { get; set; }

        /// <summary>
        /// Indication that the service is allowed in the medical referral
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
        /// Service id in E-Health
        /// </summary>
        public virtual Guid? EhealthId { get; set; }
    }
}
