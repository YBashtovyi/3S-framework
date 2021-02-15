using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Common.Attributes;
using System;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthMedicineReimbursementDto: BaseDto
    {
        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicineId { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ReimbursementProgramId { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ActivityStatusId { get; set; }

        [Display(Name = "Дата включення")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime StartDate { get; set; }

        [Display(Name = "Дата виключення")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime? EndDate { get; set; }

        [Display(Name = "Оптово-відпускна ціна за уп., грн")]
        [Required(ErrorMessage = "Заповніть поле")]
        [Range(1, 100000, ErrorMessage = "Оптово-відпускна ціна за уп. повинна бути не менше за {1} та не більше за {2}")]
        [CaseFilter(CaseFilterOperation.ValueRange)]
        public virtual double WholesalePrice { get; set; }

        [Display(Name = "Роздрібна ціна за уп., грн")]
        [Required(ErrorMessage = "Заповніть поле")]
        [Range(1, 100000, ErrorMessage = "Роздрібна ціна за уп. повинна бути не менше за {1} та не більше за {2}")]
        [CaseFilter(CaseFilterOperation.ValueRange)]
        public virtual double RetailPrice { get; set; }

        [Display(Name = "Розмір відшкодування за од.вим., грн")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.ValueRange)]
        public virtual double CompensationAmount { get; set; }

        [Display(Name = "Мінімальна кількість для реалізації")]
        [Required(ErrorMessage = "Заповніть поле")]
        [Range(1, 10000, ErrorMessage = "Кількість одиниць в упаковці повинно бути не менше за {1} та не більше за {2}")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual int MinimalRealizationAmount { get; set; }
    }
}
