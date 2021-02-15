using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Common.Attributes;
using System;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthMedicineDto: BaseDto
    {
        [Display(Name = "Торгівельна назва")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Caption { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicineManufacturerId { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicineActiveSubstanceId { get; set; }

        [Display(Name = "Кількість одиниць в упаковці")]
        [Required(ErrorMessage = "Заповніть поле")]
        [Range(1, 10000, ErrorMessage = "Кількість одиниць в упаковці повинно бути не менше за {1} та не більше за {2}")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual int AmountInPack { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicineUnitMeasuresId { get; set; }

        [Display(Name = "Добова доза лікарського засобу, рекомендована ВООЗ")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual double? RecommendedDailyDose { get; set; }

        [Display(Name = "№ РП ЛЗ")]
        [CaseFilter(CaseFilterOperation.Contains)]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string RegistrationCertificateNumber { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ActivityStatusId { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicineReleaseFormId { get; set; }
    }
}
