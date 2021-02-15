using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents profile's row level rights
    /// </summary>
    [Table("Sys" + nameof(RowLevelRight))]
    [Display(Name = "Доступ на рівні записів")]
    public class RowLevelRight: CoreEntity, ICaption
    {
        public string Caption { get; set; } = string.Empty;
        /// <summary>
        /// Link to the Profile
        /// </summary>
        public Guid ProfileId { get; set; }

        /// <summary>
        /// Entity name which records will be checked for access
        /// </summary>
        public string MainEntityName { get; set; } = string.Empty;

        /// <summary>
        /// Entity name that separates database records.
        /// </summary>
        public string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Access level to the record
        /// RowLevelAccessType.Default - a default value should be used. For example, for Organization the user's organization can be used. 
        ///     Default values should be set in UserDefaultValue table
        /// RowLevelAccessType.[Specified,Except] - white/black list should be used.
        ///     Add values to the RowLevelSecurityObject table in this case
        /// </summary>
        public RowLevelAccessType AccessType { get; set; }

        /// <summary>
        /// List of ids. Usage is defined by AccessType property
        /// </summary>
        public List<RowLevelSecurityObject> RowLevelSecurityObjects { get; set; } = new List<RowLevelSecurityObject>();
    }
}
