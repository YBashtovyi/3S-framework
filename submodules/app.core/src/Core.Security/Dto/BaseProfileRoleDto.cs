using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents a connection between profile and role
    /// </summary>
    [RightsCheckList(nameof(Profile), nameof(Role))]
    public abstract class BaseProfileRoleDto: BaseDto
    {
        /// <summary>
        /// Link to the Role
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid RoleId { get; set; }

        /// <summary>
        /// Link to the Profile
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ProfileId { get; set; }
    }
}
