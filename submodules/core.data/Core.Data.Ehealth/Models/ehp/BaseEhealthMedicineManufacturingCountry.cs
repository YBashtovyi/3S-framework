using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Довідник країн виробників лікарських засобів")]
    [Table("EhpMedicineManufacturingCountry")]
    public abstract class BaseEhealthMedicineManufacturingCountry: BaseEntity
    {
        public virtual string Code { get; set; }

        public virtual string Description { get; set; }
    }
}
