using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents external organization in E-Health
    /// </summary>
    [Display(Name = "Довідник організацій у E-Health")]
    public abstract class BaseEhealthExternalOrganization: BaseEntity
    {
        /// <summary>
        /// Organiation name in E-Health
        /// </summary>
        [MaxLength(250)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Organiation short name in E-Health
        /// </summary>
        [MaxLength(250)]
        public virtual string ShortName { get; set; }

        /// <summary>
        /// Organization type in E-Health
        /// </summary>
        public virtual Guid TypeId { get; set; }

        /// <summary>
        /// Organization edrpou code
        /// </summary>
        [MaxLength(50)]
        public virtual string Edrpou { get; set; }

        /// <summary>
        /// Organization email
        /// </summary>
        [MaxLength(250)]
        public virtual string Email { get; set; }

        /// <summary>
        /// Sign, that the organization is NHS-certified
        /// </summary>
        public virtual bool NhsVerified { get; set; }

        /// <summary>
        /// Organization id in E-Health
        /// </summary>
        public virtual Guid EhealthId { get; set; }
    }
}
