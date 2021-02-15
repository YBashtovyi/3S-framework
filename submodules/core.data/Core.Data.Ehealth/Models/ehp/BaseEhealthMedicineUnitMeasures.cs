using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Довідник одиниць виміру лікарських засобів")]
    [Table("EhpMedicineUnitMeasures")]
    public abstract class BaseEhealthMedicineUnitMeasures: BaseEntity
    {
        public virtual string Code { get; set; }
    }
}
