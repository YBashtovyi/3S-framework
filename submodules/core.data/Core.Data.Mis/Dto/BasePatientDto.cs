using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Mis.Dto
{
    public abstract class BasePatientDto: BaseDto
    {
        [MaxLength(100)]
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "По батькові")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string MiddleName { get; set; }

        [MaxLength(200)]
        [Display(Name = "Прізвище")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string LastName { get; set; }
    }

    public abstract class BasePatientListDto: BasePatientDto
    {
    }

    public abstract class BasePatientDetailDto: BasePatientDto
    {
    }
}
