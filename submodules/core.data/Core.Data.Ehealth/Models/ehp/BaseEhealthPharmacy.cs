using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Реєстр аптек")]
    [Table("EhpPharmacy")]
    public abstract class BaseEhealthPharmacy: BaseEntity
    {
        public virtual string Edrpou { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsActive { get; set; }
    }
}
