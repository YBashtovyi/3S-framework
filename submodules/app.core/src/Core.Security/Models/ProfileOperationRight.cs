using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents a connection between profile and operation right
    /// </summary>
    [Table("Sys" + nameof(ProfileOperationRight))]
    [Display(Name = "Права профілю на операцію")]
    public class ProfileOperationRight : CoreEntity
    {
        /// <summary>
        /// Link to the Profile
        /// </summary>
        public Guid ProfileId { get; set; }

        public Profile? Profile { get; set; }

        /// <summary>
        /// Link to the Operation Right
        /// </summary>
        public Guid OperationRightId { get; set; }

        public OperationRight? OperationRight { get; set; }
    }
}
