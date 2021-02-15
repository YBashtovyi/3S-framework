using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using System;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthActiveSubstanceDto: BaseDto
    {
        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid InternationalNonproprietaryNameId { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicineReleaseFormId { get; set; }

        [Display(Name = "Дозування")]
        [Required(ErrorMessage = "Заповніть поле")]
        [Range(0.000000001, 10000, ErrorMessage = "Дозування повинно бути не менше за {1} та не більше за {2}")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual double Dosage { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicineActionUnitMeasuresId { get; set; }

        [Display(Name = "Статус")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; } = true;

        [Display(Name = "ATC код")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string AnatomicalTherapeuticChemicalCode { get; set; }
    }
}
