using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents a role in the system.
    /// All rights are grouped into roles
    /// </summary>
    [RightsCheckList(nameof(Role))]
    public abstract class BaseRoleDto: BaseDto
    {
        /// <summary>
        /// Set value to false if you want to disable using this role
        /// </summary>
        [Display(Name = "Діє")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }
    }
}
