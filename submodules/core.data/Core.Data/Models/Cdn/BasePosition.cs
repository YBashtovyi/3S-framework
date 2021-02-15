using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Cdn
{
    [Display(Name = "Довідник посад")]
    [Table("CdnPosition")]
	public abstract class BasePosition : BaseDictionary
	{
    }
}
