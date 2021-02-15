using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Attributes;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthPaymentDetailDto: BaseDto
    {
        [Display(Name = "Назва банку")]
        public virtual string BankName { get; set; }
        [Display(Name = "МФО")]
        [CustomPropertyMapping("MFO")]
        public virtual string Mfo { get; set; }
        [Display(Name = "Номер рахунку")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string PayerAccount { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
