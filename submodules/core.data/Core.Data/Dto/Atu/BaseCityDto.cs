using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Dto.Atu
{
    public abstract class BaseCityDetailsDto : CoreDto
    {
        public virtual string Name { get; set; }
    }

    public abstract class BaseCityEditDto : CoreDto
    {
        public virtual string Name { get; set; }
    }

    public abstract class BaseCityListDto: CoreDto, IPagingCounted
    {
        [Display(Name = "Назва")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        public virtual int TotalRecordCount { get; set; }
    }
}
