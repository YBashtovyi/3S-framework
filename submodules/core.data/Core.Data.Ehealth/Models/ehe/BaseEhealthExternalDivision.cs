using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents external division in E-Health
    /// </summary>
    [Display(Name = "Довідник підрозділів у E-Health")]
    public abstract class BaseEhealthExternalDivision: BaseEntity
    {
        /// <summary>
        /// Division name in E-Health
        /// </summary>
        [MaxLength(250)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Division type in E-Health
        /// </summary>
        public virtual Guid TypeId { get; set; }

        /// <summary>
        /// Division id in E-Health
        /// </summary>
        public virtual Guid EhealthId { get; set; }

    }
}
