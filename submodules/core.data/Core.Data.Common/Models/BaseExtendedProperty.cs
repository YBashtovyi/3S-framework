using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Common.Models
{
    [Display(Name = "Додаткове поле")]
    public abstract class BaseExtendedProperty : BaseEntity
    {
        public virtual string Code { get; set; }
    }
}

