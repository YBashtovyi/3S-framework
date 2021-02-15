using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Ehealth declaration, that a doctor signes with a patient
    /// </summary>
    [Display(Name = "Декларація лікаря з пацієнтом")]
    [Table("EhdDeclaration")]
    public abstract class BaseEhealthDeclaration: BaseEntity
    {
        public virtual Guid? EmployeeId { get; set; }
        public virtual Guid? DivisionId { get; set; }
        public virtual string Scope { get; set; }
        public virtual bool Overlimit { get; set; }
        public virtual Guid? EhealthId { get; set; }
    }
}
