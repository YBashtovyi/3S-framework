using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Dto.System
{
    public abstract class BaseSysEvaluatedValueDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string EntityName { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual Guid EntityId { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string ValueName { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string ValueType { get; set; }

        public virtual string Value { get; set; }
    }
}
