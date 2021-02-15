using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Common
{
    [Display(Name = "Значення додаткового поля у сутності")]
    public abstract class BaseEntityExtendedPropertyValue : CoreEntity
    {
        public virtual Guid EntityId { get; set; }
        public virtual Guid PropertyId { get; set; }
        public virtual string Value { get; set; }
    }
}
