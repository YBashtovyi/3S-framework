using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents access to the concrete field
    /// </summary>
    [Table("Sys" + nameof(FieldRight))]
    [Display(Name = "Право на конкретне поле сутності")]
    public class FieldRight: CoreEntity
    {
        /// <summary>
        /// Link to the Right entity
        /// </summary>
        public Guid RightId { get; set; }

        public Right? Right { get; set; }

        /// <summary>
        /// Field name as it defined in the model
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// Access level to the field. "Access denied" by default
        /// </summary>
        public AccessLevel AccessLevel { get; set; }
    }
}
