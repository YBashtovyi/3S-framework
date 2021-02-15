using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents a connection between user and profile
    /// </summary>
    [Table("Sys" + nameof(UserProfile))]
    [Display(Name = "Профіль співробітника")]
    public class UserProfile : CoreEntity
    {
        /// <summary>
        /// Link to the user (user - is an abstract entity, it can be person, employee or user table in a database)
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Link to Profile
        /// </summary>
        public Guid ProfileId { get; set; }

        public Profile? Profile { get; set; }
    }
}
