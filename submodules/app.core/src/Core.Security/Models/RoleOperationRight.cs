using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents a connection between roles and operation rights
    /// </summary>
    [Table("Sys" + nameof(RoleOperationRight))]
    [Display(Name = "Права на операцію ролі")]
    public class RoleOperationRight : CoreEntity
    {
        /// <summary>
        /// Link to the Role
        /// </summary>
        public Guid RoleId { get; set; }

        public Role? Role { get; set; }

        /// <summary>
        /// Link to the Right
        /// </summary>
        public Guid OperationRightId { get; set; }

        public OperationRight? OperationRight { get; set; }
    }
}
