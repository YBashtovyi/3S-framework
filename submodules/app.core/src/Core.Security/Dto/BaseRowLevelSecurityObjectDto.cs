using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security.Models;

namespace Core.Security.Dto
{
    /// <summary>
    /// Represents row level right object.
    /// Add records to this table if you want to allow user access only records separated by these values
    /// This table is used when RowLevelAccessType in RowLevelRight = [Specified,Except]
    /// </summary>
    [RightsCheckList(nameof(RowLevelSecurityObject))]
    public abstract class BaseRowLevelSecurityObjectDto: CoreDto
    {
        /// <summary>
        /// Link to the RowLevelRight
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid RowLevelRightId { get; set; }

        /// <summary>
        /// Link to the entity. Entity can be in any table. Entity name is defined in RowLevelRight
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid EntityId { get; set; }
    }
}
