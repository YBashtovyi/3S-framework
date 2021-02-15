using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents a connection between user and profile
    /// </summary>
    [RightsCheckList(nameof(UserProfile))]
    public abstract class BaseUserProfileDto: BaseDto
    {
        /// <summary>
        /// Link to the user (user - is an abstract entity, it can be person, employee or user table in a database)
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid UserId { get; set; }

        /// <summary>
        /// Link to Profile
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ProfileId { get; set; }
    }
}
