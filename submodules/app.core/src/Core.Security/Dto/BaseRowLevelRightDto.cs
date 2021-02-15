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
    /// Represents profile's row level rights
    /// </summary>
    [RightsCheckList(nameof(RowLevelRight))]
    public abstract class BaseRowLevelRightDto: BaseDto
    {
        /// <summary>
        /// Link to the Profile
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ProfileId { get; set; }

        /// <summary>
        /// Entity name which records will be checked for access
        /// </summary>
        [Display(Name = "Назва основної сутності")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public string MainEntityName { get; set; } = string.Empty;

        /// <summary>
        /// Entity name that separates database records.
        /// </summary>
        [Display(Name = "Назва сутності")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Access level to the record
        /// RowLevelAccessType.Default - a default value should be used. For example, for Organization the user's organization can be used. 
        ///     Default values should be set in UserDefaultValue table
        /// RowLevelAccessType.[Specified,Except] - white/black list should be used.
        ///     Add values to the RowLevelSecurityObject table in this case
        /// </summary>
        [Display(Name = "Рівень доступу на рівні записів")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual RowLevelAccessType AccessType { get; set; }
    }
}
