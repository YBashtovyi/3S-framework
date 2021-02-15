using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using System;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Довідник діючих речовин")]
    [Table("EhpActiveSubstance")]
    public abstract class BaseEhealthActiveSubstance: BaseEntity
    {
        public virtual Guid InternationalNonproprietaryNameId { get; set; }

        public virtual Guid MedicineReleaseFormId { get; set; }

        public virtual double Dosage { get; set; }

        public virtual Guid MedicineActionUnitMeasuresId { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual string AnatomicalTherapeuticChemicalCode { get; set; }

    }
}
