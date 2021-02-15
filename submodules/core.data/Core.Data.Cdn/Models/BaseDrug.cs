using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Cdn.Models
{
    [Display(Name = "Довідник лікарских засобів")]
    [Table("CdnDrug")]
    public abstract class BaseDrug: BaseDirectory
    {
    }
}
