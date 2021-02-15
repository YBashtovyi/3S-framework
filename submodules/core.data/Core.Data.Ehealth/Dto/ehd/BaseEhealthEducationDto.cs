using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthEducationDto: BaseDto
    {
        [Display(Name = "Країна")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string CountryCode { get; set; }
        [Display(Name = "Місто навчання")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string City { get; set; }
        [Display(Name = "Назва навчального закладу")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string InstitutionName { get; set; }
        [Display(Name = "Дата отримання")]
        [DataType(DataType.Date)]
        public virtual DateTime? IssuedDate { get; set; }
        [Display(Name = "Серія/Номер диплому")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string DiplomaNumber { get; set; }
        [Display(Name = "Освітньо-кваліфікаційний рівень")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string DegreeCode { get; set; }
        [Display(Name = "Спеціальність")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string SpecialityCode { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
