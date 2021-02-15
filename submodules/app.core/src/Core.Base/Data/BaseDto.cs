using System;
using System.ComponentModel.DataAnnotations;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Base.Data
{
    public abstract class BaseDto: CoreDto
    {
        [Display(Name = "Назва")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Caption { get; set; }
    }
}
