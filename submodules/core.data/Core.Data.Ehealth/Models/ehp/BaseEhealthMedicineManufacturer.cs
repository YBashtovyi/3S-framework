using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Довідник виробників лікарських засобів")]
    [Table("EhpMedicineManufacturer")]
    public abstract class BaseEhealthMedicineManufacturer: BaseEntity
    {
        public virtual Guid ManufacturerTypeId { get; set; }
        
        public virtual string Edrpou { get; set; }

        public virtual Guid ManufacturingCountryId { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsActive { get; set; }

    }
}
