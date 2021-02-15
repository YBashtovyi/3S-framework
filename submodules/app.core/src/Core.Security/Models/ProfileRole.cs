using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents a connection between profile and role
    /// </summary>
    [Table("Sys" + nameof(ProfileRole))]
    [Display(Name = "Роль у профілі")]
    public class ProfileRole : CoreEntity
    {
        /// <summary>
        /// Link to the Profile
        /// </summary>
        public Guid ProfileId { get; set; }

        public Profile? Profile { get; set; }

        /// <summary>
        /// Link to the Role
        /// </summary>
        public Guid RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
