using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Cdn.Dto
{
    public abstract class BaseSpecialityDto: BaseDto
    {
    }

    public abstract class BaseSpecialityDetailDto : BaseSpecialityDto
    {
        [Display(Name = "Опис")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Description { get; set; }
    }

    public abstract class BaseSpecialityListDto: BaseSpecialityDto, IPagingCounted
    {
        public virtual int TotalRecordCount { get; set; }
    }

    public abstract class BaseSpecialitySelectDto: BaseSpecialityDto
    {
    }
}
