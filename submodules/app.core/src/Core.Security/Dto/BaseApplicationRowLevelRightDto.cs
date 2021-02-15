using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents existing row level rights in the application.
    /// </summary>
    [RightsCheckList(nameof(ApplicationRowLevelRight))]
    public abstract class BaseApplicationRowLevelRightDto: BaseDto
    {
        /// <summary>
        /// Name of the entity that separates access to the table
        /// For example, for EntityName = "Organization" access to application tables will be separated by organizations
        /// In such case an allowed organizations list should be defined for every user 
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        [Display(Name = "Назва сутності")]
        public virtual string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Set value to false if you want to disable using this application row level right
        /// </summary>
        [Display(Name = "Діє")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }
    }
}
