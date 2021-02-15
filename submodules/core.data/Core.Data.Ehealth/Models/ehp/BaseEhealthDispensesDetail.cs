using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Інформація про погашення рецепту")]
    [Table("EhpDispensesDetail")]
    public abstract class BaseEhealthDispensesDetail: BaseEntity
    {
        public virtual Guid PrescriptionId { get; set; }
        public virtual string ManufacturerName { get; set; }
        public virtual string ManufacturerCountry { get; set; }
        public virtual Guid? EhealthId { get; set; }
        public virtual string MedicationName { get; set; }
        public virtual double MedicationQuantity { get; set; }
    }
}
