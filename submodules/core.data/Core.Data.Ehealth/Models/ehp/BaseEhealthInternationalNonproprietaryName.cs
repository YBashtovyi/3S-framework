using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Довідник міжнародних непатентованих назв (МНН)")]
    [Table("EhpInternationalNonproprietaryName")]
    public abstract class BaseEhealthInternationalNonproprietaryName: BaseEntity
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string NameOriginal { get; set; }
    }
}
