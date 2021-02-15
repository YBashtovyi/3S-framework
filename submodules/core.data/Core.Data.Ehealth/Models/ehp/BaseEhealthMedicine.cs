using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Реєстр готових лікарських засобів")]
    [Table("EhpMedicines")]
    public abstract class BaseEhealthMedicine: BaseEntity
    {

        public virtual Guid MedicineManufacturerId { get; set; }

        public virtual Guid MedicineActiveSubstanceId { get; set; }

        public virtual int AmountInPack { get; set; }
        
        public virtual Guid MedicineUnitMeasuresId { get; set; }

        public virtual double? RecommendedDailyDose { get; set; }

        public virtual string RegistrationCertificateNumber { get; set; }

        public virtual DateTime? StartDate { get; set; }
        
        public virtual DateTime? EndDate { get; set; }

        public virtual bool IsUnlimited { get; set; }

        public virtual Guid ActivityStatusId { get; set; }

        public virtual Guid MedicineReleaseFormId { get; set; }        
    }
}
