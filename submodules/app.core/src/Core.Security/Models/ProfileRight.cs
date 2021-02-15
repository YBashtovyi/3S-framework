using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents a connection between profile and right
    /// </summary>
    [Table("Sys" + nameof(ProfileRight))]
    [Display(Name = "Права профілю")]
    public class ProfileRight : CoreEntity
    {
        /// <summary>
        /// Link to the Profile
        /// </summary>
        public Guid ProfileId { get; set; }

        public Profile? Profile { get; set; }

        /// <summary>
        /// Link to the Right
        /// </summary>
        public Guid RightId { get; set; }

        public Right? Right { get; set; }
    }
}
