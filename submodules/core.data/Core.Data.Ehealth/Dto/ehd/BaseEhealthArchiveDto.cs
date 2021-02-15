using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthArchiveDto: BaseDto
    {
        [Display(Name = "Дата передачі паперових документів до архіву")]
        [Required(ErrorMessage = "Заповніть поле")]
        [DataType(DataType.Date)]
        public virtual DateTime? Date { get; set; } = DateTime.Now;
        [Display(Name = "Адреса будівлі, де знаходяться паперові документи")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string Place { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
