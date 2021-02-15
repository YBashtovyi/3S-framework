using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Atu.Models
{
    [Display(Name = "Довідник міст")]
    [Table("AtuCity")]
    public abstract class BaseCity : BaseEntity
	{
	    public virtual string Code { get; set; }
	    public virtual Guid TypeId { get; set; }
        public virtual Guid RegionId { get; set; }
    }
}
