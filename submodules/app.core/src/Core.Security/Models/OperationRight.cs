using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents an operation right in the system. 
    /// </summary>
    [Table("Sys" + nameof(OperationRight))]
    [Display(Name = "Право на операцію")]
    public class OperationRight: CoreEntity, ICaption
    {
        public string Caption { get; set; } = string.Empty;
        /// <summary>
        /// Name of an operation
        /// </summary>
        public string OperationName { get; set; } = string.Empty;

        /// <summary>
        /// Access level to the operation.
        /// No - an operation is not allowed, the operation button in views is not visible
        /// Read - an operation is not allowed, the operation button in views is visible but in read-only mode
        /// Write - an operation is allowed, the operation button in views is visible and clickable
        /// </summary>
        public AccessLevel AccessLevel { get; set; } = AccessLevel.No;

        /// <summary>
        /// Set value to false if you want to disable using this right
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// List of roles where this right is present
        /// </summary>
        public List<RoleOperationRight> Roles { get; set; } = new List<RoleOperationRight>();

        /// <summary>
        /// List of profiles where this right is present
        /// </summary>
        public List<ProfileOperationRight> Profiles { get; set; } = new List<ProfileOperationRight>();
    }
}
