using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Common eHealth document.
    /// Is used in several eHealth entities
    /// </summary>
    [Display(Name = "Загальний документ eHealth", Description = "Використовується в організації, підрозділі, співробітнику, пацієнта декларації")]
    [Table("EhdDocument")]
    public abstract class BaseEhealthDocument: BaseEntity
    {
        public virtual string TypeCode { get; set; }
        public virtual string Number { get; set; }
        public virtual string IssuedBy { get; set; }
        public virtual DateTime? IssuedAt { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
