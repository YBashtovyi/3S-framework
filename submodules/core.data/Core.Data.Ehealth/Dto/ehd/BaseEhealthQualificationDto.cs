using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthQualificationDto: BaseDto
    {
        [Display(Name = "Тип")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string TypeCode { get; set; }
        [Display(Name = "Назва навчального закладу")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string InstitutionName { get; set; }
        [Display(Name = "Спеціальність")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string SpecialityCode { get; set; }
        [Display(Name = "Дата отримання")]
        [DataType(DataType.Date)]
        public virtual DateTime? IssuedDate { get; set; }
        [Display(Name = "Номер сертифікату")]
        public virtual string CertificateNumber { get; set; }
        [Display(Name = "Дійсний до")]
        [DataType(DataType.Date)]
        public virtual DateTime? ValidTo { get; set; }
        [Display(Name = "Додаткова інформація")]
        public virtual string AdditionalInfo { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
