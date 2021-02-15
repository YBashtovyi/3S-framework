using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthDocumentDto: BaseDto
    {
        [Display(Name = "Тип документу")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string TypeCode { get; set; }
        [Display(Name = "Номер документу")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string Number { get; set; }
        [Display(Name = "Ким видано")]
        public virtual string IssuedBy { get; set; }
        [Display(Name = "Дата видачі документу")]
        [DataType(DataType.Date)]
        public virtual DateTime? IssuedAt { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
