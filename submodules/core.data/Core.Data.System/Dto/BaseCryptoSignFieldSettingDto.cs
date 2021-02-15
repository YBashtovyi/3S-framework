using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.System.Dto
{
    public abstract class BaseCryptoSignFieldSettingDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        [Display(Name = "Сутність")]
        [Required]
        public virtual string EntityName { get; set; }
        [CaseFilter(CaseFilterOperation.Contains)]
        [Display(Name = "Назва поля")]
        [Required]
        public virtual string FieldName { get; set; }
        [CaseFilter(CaseFilterOperation.Contains)]
        [Display(Name = "Назва поля у підписаних даних")]
        public virtual string SignFieldName { get; set; }
        [CaseFilter(CaseFilterOperation.ValueRange)]
        [Display(Name = "Дата початку дії налаштування")]
        public virtual DateTime DateCreated { get; set; }
    }
}
