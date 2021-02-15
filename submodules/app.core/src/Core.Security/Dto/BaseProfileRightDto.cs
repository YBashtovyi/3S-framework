using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents a connection between profile and right
    /// </summary>
    [RightsCheckList(nameof(Profile), nameof(Right))]
    public abstract class BaseProfileRightDto: BaseDto
    {
        /// <summary>
        /// Link to the Right
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid RightId { get; set; }

        /// <summary>
        /// Link to the Profile
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ProfileId { get; set; }
    }
}
