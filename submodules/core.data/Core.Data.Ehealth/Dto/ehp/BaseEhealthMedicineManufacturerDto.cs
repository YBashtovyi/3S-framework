using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Common.Attributes;
using System;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthMedicineManufacturerDto: BaseDto
    {
        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ManufacturerTypeId { get; set; }

        [Display(Name = "Назва виробника")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Caption { get; set; }

        [Display(Name = "ЄДРПОУ")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Edrpou { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ManufacturingCountryId { get; set; }

        [Display(Name = "Примітка")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Description { get; set; }

        [Display(Name = "Статус")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; } = true;
    }
}
