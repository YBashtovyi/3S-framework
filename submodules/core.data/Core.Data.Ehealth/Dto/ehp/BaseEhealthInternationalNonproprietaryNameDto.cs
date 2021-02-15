using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Common.Attributes;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthInternationalNonproprietaryNameDto: BaseDto
    {
        [Display(Name = "Код")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Code { get; set; }

        [Display(Name = "Назва (укр.)")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        [Display(Name = "Назва (англ.)")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string NameOriginal { get; set; }
    }
}
