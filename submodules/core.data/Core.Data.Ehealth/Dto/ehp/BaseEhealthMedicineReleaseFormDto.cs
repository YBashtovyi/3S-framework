using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Common.Attributes;
using System;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthMedicineReleaseFormDto: BaseDto
    {
        [Display(Name = "Код в Ehealth")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Code { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Caption { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? ParentId { get; set; }
    }
}
