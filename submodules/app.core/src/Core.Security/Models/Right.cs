using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents right in the system. 
    /// Every right is set for entity that represents a table in a database
    /// </summary>
    [Table("Sys" + nameof(Right))]
    [Display(Name = "Право")]
    public class Right : CoreEntity
    {
        public string Caption { get; set; } = string.Empty;
        /// <summary>
        /// Entity name that is a subject of the right. Use nameof(YourEntityClass) for avoiding mistakes
        /// </summary>
        public string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Access level to the entity.
        /// Use "Partial" if it is necessary to set rights by field.
        /// In "Partial" case you should create FieldRight record for fields, you want to give access. Otherwise, access will be denied
        /// </summary>
        public EntityAccessLevel EntityAccessLevel { get; set; } = EntityAccessLevel.No;

        /// <summary>
        /// Set value to false if you want to disable using this right
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Field rights for this right (if partial EntityAccessLevel is set)
        /// </summary>
        public List<FieldRight> FieldRights { get; set; } = new List<FieldRight>();

        /// <summary>
        /// List of roles where this right is present
        /// </summary>
        public List<RoleRight> Roles { get; set; } = new List<RoleRight>();

        /// <summary>
        /// List of profiles where this right is present
        /// </summary>
        public List<ProfileRight> Profiles { get; set; } = new List<ProfileRight>();
    }
}
