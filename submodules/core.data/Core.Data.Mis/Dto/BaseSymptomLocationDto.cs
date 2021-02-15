using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Mis.Dto
{
    public abstract class BaseSymptomLocationDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid? ParentId { get; set; }
    }

    public abstract class BaseSymptomLocationListDto: BaseSymptomLocationDto
    {

    }

    public abstract class BaseSymptomLocationDetailDto: BaseSymptomLocationDto
    {
        [Display(Name = "Підпорядкування")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParentCaption { get; set; }
    }
}
