using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Довідник сили дії лікарських засобів")]
    [Table("EhpMedicineActionUnitMeasures")]
    public abstract class BaseEhealthMedicineActionUnitMeasures: BaseEntity
    {
        public virtual string Code { get; set; }
    }
}
