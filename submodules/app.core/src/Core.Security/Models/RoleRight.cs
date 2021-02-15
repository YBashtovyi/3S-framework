using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents a connection between roles and rights
    /// </summary>
    [Table("Sys" + nameof(RoleRight))]
    [Display(Name = "Права ролі")]
    public class RoleRight : CoreEntity
    {
        /// <summary>
        /// Link to the Role
        /// </summary>
        public Guid RoleId { get; set; }

        public Role? Role { get; set; }

        /// <summary>
        /// Link to the Right
        /// </summary>
        public Guid RightId { get; set; }

        public Right? Right { get; set; }
    }
}
