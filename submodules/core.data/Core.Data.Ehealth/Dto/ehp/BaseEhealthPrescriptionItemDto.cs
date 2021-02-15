using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Common.Attributes;
using System;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthPrescriptionItemDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid PrescriptionId { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ActiveSubstanceId { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? MedicineId { get; set; }

        [Display(Name = "Кількість (доз/прийом)")]
        [Required(ErrorMessage = "Заповніть поле")]
        [Range(1, 10000, ErrorMessage = "Кількість доз за прийом повинна бути не менше за {1} та не більше за {2}")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual int Quantity { get; set; }

        [Display(Name = "Кратність (разів на день)")]
        [Required(ErrorMessage = "Заповніть поле")]
        [Range(1, 100, ErrorMessage = "Кількість прийомів на день повинна бути не менше за {1} та не більше за {2}")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual int TimesDuringDay { get; set; }

        [Display(Name = "Тривалість (днів)")]
        [Required(ErrorMessage = "Заповніть поле")]
        [Range(1, 90, ErrorMessage = "Тривалість курсу повинна бути не менше за {1} та не більше за {2}")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual int Duration { get; set; } = 1;

        [Display(Name = "Сумарна кількість доз")]
        [Required(ErrorMessage = "Заповніть поле")]
        [Range(1, 10000, ErrorMessage = "Кількість доз повинна бути не менше за {1} та не більше за {2}")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual int TotalDosesQuantity { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicineTakingMethodId { get; set; }

        [Display(Name = "Опис")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Description { get; set; }
    }
}
