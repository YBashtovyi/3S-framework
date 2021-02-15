using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthBaseContractDto: BaseDto
    {
        [Display(Name = "№ контракту")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string Number { get; set; }
        [Display(Name = "Дата з")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual DateTime IssuedAt { get; set; }
        [Display(Name = "Дата по")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual DateTime ExpiresAt { get; set; }
        public virtual Guid EntityId { get; set; }
    }
}
