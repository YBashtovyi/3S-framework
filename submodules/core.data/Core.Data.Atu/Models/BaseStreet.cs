using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Atu.Models
{
    [Display(Name = "Довідник вулиць")]
    [Table("AtuStreet")]
    public abstract class BaseStreet : BaseEntity
	{
        public virtual Guid CityId { get; set; }
    }
}
