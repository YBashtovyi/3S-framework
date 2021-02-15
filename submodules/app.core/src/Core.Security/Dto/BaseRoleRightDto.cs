using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents a connection between roles and rights
    /// </summary>
    [RightsCheckList(nameof(Role), nameof(Right))]
    public abstract class BaseRoleRightDto: BaseDto
    {
        /// <summary>
        /// Link to the Role
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid RoleId { get; set; }

        /// <summary>
        /// Link to the Right
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid RightId { get; set; }
    }
}
