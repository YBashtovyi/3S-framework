using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents security profile in the system.
    /// All the application roles, rights, rls rights are gathered into profiles
    /// </summary>
    [Table("Sys"+nameof(Profile))]
    [Display(Name = "Профіль")]
    public class Profile : CoreEntity, ICaption
    {
        public string Caption { get; set; } = string.Empty;
        /// <summary>
        /// Set value to false if you want to disable using this profile
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// List of roles that this profile contains
        /// </summary>
        public List<ProfileRole> Roles { get; set; } = new List<ProfileRole>();

        /// <summary>
        /// List of rights that this profile contains
        /// </summary>
        public List<ProfileRight> Rights { get; set; } = new List<ProfileRight>();

        /// <summary>
        /// List of row level rigths that this profile contains
        /// </summary>
        public List<RowLevelRight> RowLevelRights { get; set; } = new List<RowLevelRight>();

        /// <summary>
        /// List of operation rights that this profile contains
        /// </summary>
        public List<ProfileOperationRight> OperationRights { get; set; } = new List<ProfileOperationRight>();

        /// <summary>
        /// Type id for profile
        /// </summary>
        ///<remarks>
        /// Used for extra analytics. Add foreign key to linked table in DbContext OnModelCreating method
        ///</remarks>
        public Guid? TypeId { get; set; }

    }
}
