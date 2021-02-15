using Core.Base.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Data.Org.Models
{
    [Display(Name = "Співробітник")]
    public abstract class BaseEmployee : BaseEntity
    {
        [Display(Name = "Персона")]
        public virtual Guid PersonId { get; set; }
        public virtual Guid OrganizationId { get; set; }
    }
}
