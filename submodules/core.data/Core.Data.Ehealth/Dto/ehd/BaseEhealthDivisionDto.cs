using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthDivisionDto: BaseDto
    {
        [Display(Name = "Назва підрозділу")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string Name { get; set; }
        [Display(Name="E-mail відділення")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string Email { get; set; }
        [Display(Name = "Тип підрозділу")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string TypeCode { get; set; }
        public virtual string ExternalId { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual decimal LocationLatitude { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual decimal LocationLongitude { get; set; }
        public virtual Guid? EhealthId { get; set; }
    }
}
