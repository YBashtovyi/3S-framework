using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Ehealth phone.
    /// Is used in many eHealth entities
    /// </summary>
    [Display(Name = "Телефон eHealth", Description = "Використовується в організації, підрозділі, співробітнику, пацієнта декларації")]
    [Table("EhdPhone")]
    public abstract class BaseEhealthPhone: BaseEntity
    {
        public virtual string TypeCode { get; set; }
        public virtual string Number { get; set; }
        public virtual Guid EntityId { get; set; }
    }
}
