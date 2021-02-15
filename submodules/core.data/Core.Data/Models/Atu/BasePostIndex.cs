using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Atu
{
    [Display(Name = "Довідник поштових індексів")]
    [Table("AtuPostIndex")]
    public abstract class BasePostIndex: CoreEntity
    {
        public virtual Guid CityId { get; set; }
        public virtual string PostIndexStr { get; set; }
    }
}
