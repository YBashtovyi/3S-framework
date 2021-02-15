using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Довідник умов оплати")]
    [Table("EhpPaymentTerms")]
    public abstract class BaseEhealthPaymentTerms: BaseEntity
    {
        public virtual string Code { get; set; }

        public virtual bool IsActive { get; set; }
    }
}
