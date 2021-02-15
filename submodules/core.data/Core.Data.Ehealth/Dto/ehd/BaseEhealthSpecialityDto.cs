using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthSpecialityDto: BaseDto
    {
        [Display(Name = "Спеціальність")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string SpecialityCode { get; set; }
        [Display(Name = "Спеціальність за посадою")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual bool SpecialityOfficio { get; set; }
        [Display(Name = "Категорія")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string LevelCode { get; set; }
        [Display(Name = "Тип кваліфікації")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string QualificationTypeCode { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string AttestationName { get; set; }
        [Display(Name = "Дата отримання")]
        [DataType(DataType.Date)]
        public virtual DateTime? AttestationDate { get; set; }
        [Display(Name = "Дійсний до")]
        [DataType(DataType.Date)]
        public virtual DateTime? ValidToDate { get; set; }
        [Display(Name = "Номер сертифікату")]
        public virtual string CertificateNumber { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
