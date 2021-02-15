using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthPhoneDto: BaseDto
    {
        [Display(Name = "Тип номеру")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string TypeCode { get; set; }
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string Number { get; set; }
        public virtual Guid EntityId { get; set; }
    }
}
