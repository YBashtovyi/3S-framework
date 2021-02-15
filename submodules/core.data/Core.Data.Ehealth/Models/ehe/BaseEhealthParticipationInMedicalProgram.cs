using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents which program contains the service catalog element
    /// </summary>
    [Display(Name = "Участь у програмі")]
    public abstract class BaseEhealthParticipationInMedicalProgram : BaseEntity
    {
        /// <summary>
        /// Medical service program id in application
        /// </summary>
        public virtual Guid MedicalServiceProgramId { get; set; }

        /// <summary>
        /// Service catalog group id in application
        /// </summary>
        public virtual Guid? ServiceCatalogGroupId { get; set; }

        /// <summary>
        /// Service catalog service id in application
        /// </summary>
        public virtual Guid? ServiceCatalogServiceId { get; set; }

        /// <summary>
        /// Indication that the participation in medical program is active or inactive
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Indication that the participation in medical program is allowed in the medical referral
        /// </summary>
        public virtual bool RequestAllowed { get; set; }

        /// <summary>
        /// Consumer price of participation in medical program 
        /// </summary>
        public virtual decimal ConsumerPrice { get; set; }

        /// <summary>
        /// Description of participation in medical program 
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Date of creation in E-Health
        /// </summary>
        public virtual DateTime InsertedAtInEhealth { get; set; }

        /// <summary>
        /// Date of updated in E-Health
        /// </summary>
        public virtual DateTime? UpdatedAtInEhealth { get; set; }

        /// <summary>
        /// Identifier of participation in medical program in E-Health
        /// </summary>
        public virtual Guid? EhealthId { get; set; }
    }
}
