using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Класифікатор \"Категорії показників\"")]
    [Table("EhpObservationCategory")]
    public class  BaseEhealthObservationCategory: BaseEntity
    {
        public virtual string EhealthCode { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }
    }
}
