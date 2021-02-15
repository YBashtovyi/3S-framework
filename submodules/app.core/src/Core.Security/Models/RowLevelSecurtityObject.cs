using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents row level right object.
    /// Add records to this table if you want to allow user access only records separated by these values
    /// This table is used when RowLevelAccessType in RowLevelRight = [Specified,Except]
    /// </summary>
    [Table("Sys" + nameof(RowLevelSecurityObject))]
    [Display(Name = "Об'єкт рівня доступу")]
    public class RowLevelSecurityObject: CoreEntity
    {
        /// <summary>
        /// Link to the RowLevelRight
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid RowLevelRightId { get; set; }

        public RowLevelRight? RowLevelRight { get; set; }

        /// <summary>
        /// Link to the entity. Entity can be in any table. Entity name is defined in RowLevelRight
        /// </summary>
        public Guid EntityId { get; set; }
    }
}
