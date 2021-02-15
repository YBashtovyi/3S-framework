using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Common.Attributes;
using System;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseReimbursementProgramDto: BaseDto
    {
        [Display(Name = "Код")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Code { get; set; }

        [Display(Name = "Назва")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Caption { get; set; }

        [Required(ErrorMessage = "Ehealth програма відшкодування")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsEhealthProgram { get; set; } = true;

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? EhealthMedicalServiceProgramId { get; set; }
        
        [Display(Name = "Дата початку")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Дата закінчення")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime? EndDate { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ActivityStatusId { get; set; }

        [Display(Name = "Опис")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Description { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicineSearchTypeId { get; set; }

        [Display(Name = "Максимальна кількість позицій в рецепті")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual int PrescriptionMaxItemsCount { get; set; }

        [Display(Name = "Максимальна тривалість курсу(днів)")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual int MaxCourseDuration { get; set; }

        [Display(Name = "Бажано обрати аптеку")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsPharmacyDesirable { get; set; }

        [Display(Name = "Заборона збережнння рецепта якщо вже є курс з таким МНН")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool ProhibitSavingSameInn { get; set; }

    }
}
