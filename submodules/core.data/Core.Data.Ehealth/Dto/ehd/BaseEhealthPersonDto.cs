using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthPersonDto: BaseDto
    {
        [Display(Name = "Ім'я")]
        // [Required(ErrorMessage = "Заповніть поле")]
        public virtual string FirstName { get; set; }
        [Display(Name = "Прізвище")]
        // [Required(ErrorMessage = "Заповніть поле")]
        public virtual string LastName { get; set; }
        [Display(Name = "По батькові")]
        public virtual string SecondName { get; set; }
    }
}
