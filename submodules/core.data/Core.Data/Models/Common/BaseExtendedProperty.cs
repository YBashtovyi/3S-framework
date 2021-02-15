using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Common
{
    [Display(Name = "Додаткове поле")]
    public abstract class BaseExtendedProperty : CoreEntity, ICaption
    {
        public virtual string Caption { get; set; }
        public virtual string Code { get; set; }
    }
}

