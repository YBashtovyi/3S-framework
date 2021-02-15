using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents access to the concrete field
    /// </summary>
    [RightsCheckList(nameof(FieldRight))]
    public abstract class BaseFieldRightDto: BaseDto
    {
        /// <summary>
        /// Link to the Right entity
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid RightId { get; set; }

        /// <summary>
        /// Field name as it defined in the model
        /// </summary>
        [Display(Name = "Назва поля")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// Access level to the field. "Access denied" by default
        /// </summary>
        [Display(Name = "Рівень доступу")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual AccessLevel AccessLevel { get; set; } = AccessLevel.No;
    }
}
