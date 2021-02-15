using Core.Base.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Data.Models.Org
{
    [Table("OrgEmployee")]
    [Display(Name = "Співробітник")]
    public abstract class BaseEmployee : CoreEntity
    {
        public virtual Guid PersonId { get; set; }
    }
}
