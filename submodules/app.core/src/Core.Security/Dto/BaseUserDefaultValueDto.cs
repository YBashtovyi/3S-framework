using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents user default values used for row level access
    /// </summary>
    [RightsCheckList(nameof(UserDefaultValue))]
    public abstract class BaseUserDefaultValueDto: CoreDto
    {
        /// <summary>
        /// Name of the enity that the default value referres to
        /// </summary>
        [Display(Name = "Назва сутності")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Link to the application user, that has default values
        /// It can be employee, user, person etc and is defined in the model.
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid UserId { get; set; }

        /// <summary>
        /// Id of the concrete record in the table defined by EntityName property
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ValueId { get; set; }
    }
}
