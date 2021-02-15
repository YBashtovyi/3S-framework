using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents user default values used for row level access
    /// </summary>
    [Table("Sys" + nameof(UserDefaultValue))]
    [Display(Name = "Значення користувача за замовченням для використання в правах на рівні запису")]
    public class UserDefaultValue: CoreEntity
    {
        /// <summary>
        /// Name of the enity that the default value referres to
        /// </summary>
        public string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Link to the application user, that has default values
        /// It can be employee, user, person etc and is defined in the model.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Id of the concrete record in the table defined by EntityName property
        /// </summary>
        public Guid ValueId { get; set; }
    }
}
