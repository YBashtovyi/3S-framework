using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents right in the system. 
    /// Every right is set for entity that represents a table in a database
    /// </summary>
    [RightsCheckList(nameof(Right))]
    public abstract class BaseRightDto: BaseDto
    {
        /// <summary>
        /// Entity name that is a subject of the right. Use nameof(YourEntityClass) for avoiding mistakes
        /// </summary>
        [Display(Name = "Назва сутності")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Access level to the entity.
        /// Use "Partial" if it is necessary to set rights by field.
        /// In "Partial" case you should create FieldRight record for fields, you want to give access. Otherwise, access will be denied
        /// </summary>
        [Display(Name = "Рівень доступу")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual EntityAccessLevel EntityAccessLevel { get; set; } = EntityAccessLevel.No;

        /// <summary>
        /// Set value to false if you want to disable using this right
        /// </summary>
        [Display(Name = "Діє")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }
    }
}
