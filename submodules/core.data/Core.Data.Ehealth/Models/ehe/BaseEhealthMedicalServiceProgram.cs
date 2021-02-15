using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Reference book of medical service program in E-Health
    /// </summary>
    [Display(Name = "Класифікатор програм з E-Health")]
    [Table("EheMedicalServiceProgram")]
    public abstract class BaseEhealthMedicalServiceProgram: BaseEntity
    {
        /// <summary>
        /// Sign, that program is active in eHealth
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Hidden for user selection
        /// </summary>
        public virtual bool Hidden { get; set; }

        /// <summary>
        /// Medical service program id in E-Health
        /// </summary>
        public virtual Guid EhealthId { get; set; }
    }
}
