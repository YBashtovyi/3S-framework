using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents a role in the system.
    /// All rights are grouped into roles
    /// </summary>
    [Table("Sys" + nameof(Role))]
    [Display(Name = "Роль")]
    public class Role : CoreEntity, ICaption
    {
        public string Caption { get; set; } = string.Empty;
        /// <summary>
        /// Set value to false if you want to disable using this role
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// List of profiles, where this role is present
        /// </summary>
        public List<ProfileRole> Profiles { get; set; } = new List<ProfileRole>();

        /// <summary>
        /// List of rigths, that this role contains
        /// </summary>
        public List<RoleRight> Rights { get; set; } = new List<RoleRight>();

        /// <summary>
        /// List of operation rights, that this role contains
        /// </summary>
        public List<RoleOperationRight> OperationRights { get; set; } = new List<RoleOperationRight>();
    }
}
