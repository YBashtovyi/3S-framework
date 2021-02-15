using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents security profile in the system.
    /// All the application roles, rights, rls rights are gathered into profiles
    /// </summary>
    [RightsCheckList(nameof(Profile))]
    public abstract class BaseProfileDto: BaseDto
    {
        /// <summary>
        /// Set value to false if you want to disable using this profile
        /// </summary>
        [Display(Name = "Діє")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }
    }
}
