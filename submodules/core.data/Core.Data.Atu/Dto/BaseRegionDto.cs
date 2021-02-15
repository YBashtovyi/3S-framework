using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Atu.Dto
{
    public abstract class BaseRegionListDto: BaseDto, IPagingCounted
    {
        public virtual int TotalRecordCount { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Caption { get; set; }

        [Display(Name = "Код")]
        public virtual string Code { get; set; }

        [Display(Name = "Посилання на батьківський запис")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? ParentId { get; set; }

        [Display(Name = "Посилання на батьківський запис")]
        public virtual string ParentCaption { get; set; }

        public virtual Guid CountryId { get; set; }

        [Display(Name = "Країна")]
        public virtual string CountryCaption { get; set; }
    }

    public abstract class BaseRegionSelectDto: BaseDto
    {
        [Display(Name = "Посилання на батьківський запис")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? ParentId { get; set; }
    }
}
