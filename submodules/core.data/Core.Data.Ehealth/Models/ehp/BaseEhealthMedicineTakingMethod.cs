using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Довідник способів прийняття ліків")]
    [Table("EhpMedicineTakingMethod")]
    public abstract class BaseEhealthMedicineTakingMethod: BaseEntity
    {
        public virtual string Code { get; set; }
    }
}
