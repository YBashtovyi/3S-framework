using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Реєстр пунктів рецепта")]
    [Table("EhpPrescriptionItem")]
    public abstract class BaseEhealthPrescriptionItem: BaseEntity
    {
        public virtual Guid? MedicineId { get; set; }

        public virtual Guid PrescriptionId { get; set; }

        public virtual Guid ActiveSubstanceId { get; set; }

        public virtual int Quantity { get; set; }

        public virtual int TimesDuringDay { get; set; }

        public virtual int Duration { get; set; }

        public virtual int TotalDosesQuantity { get; set; }

        public virtual Guid MedicineTakingMethodId { get; set; }

        public virtual string Description { get; set; }        
    }
}
