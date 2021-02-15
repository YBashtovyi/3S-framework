using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using System;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Довідник форм випуску лікарських засобів")]
    [Table("EhpMedicineReleaseForm")]
    public abstract class BaseEhealthMedicineReleaseForm: BaseEntity
    {
        public virtual string Code { get; set; }
        public virtual Guid? ParentId { get; set; }
    }
}
