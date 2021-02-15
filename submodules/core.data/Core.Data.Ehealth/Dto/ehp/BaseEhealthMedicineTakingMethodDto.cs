﻿using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Common.Attributes;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthMedicineTakingMethodDto: BaseDto
    {
        [Display(Name = "Код")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Code { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Caption { get; set; }
    }
}
