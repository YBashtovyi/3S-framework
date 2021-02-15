using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    public class BaseEhealthObservationCategoryDto:  BaseDto
    {
        [Display(Name = "Код")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Code { get; set; }

        [Display(Name = "Код в E-health")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string EhealthCode { get; set; }

        [Display(Name = "Назва (укр.)")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }
    }
}
