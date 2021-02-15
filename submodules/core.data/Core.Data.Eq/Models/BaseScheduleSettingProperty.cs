using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Eq.Models
{
    /// <summary>
    /// Specific properties for scheduling.
    /// </summary>
    [Table("EqScheduleSettingProperty")]
    public abstract class BaseScheduleSettingProperty: CoreEntity
    {
        /// <summary>
        /// Id from the schedule settings table.
        /// </summary>
        public virtual Guid ScheduleSettingId { get; set; }

        /// <summary>
        /// Type of schedule specific properties.
        /// </summary>
        /// <remarks>
        /// Specific properties that are not added to the schedule settings table. For example, you can use <see cref="BaseEnumRecord"/> whose name is - <c>SchedulePropertyType</c>.
        /// </remarks>
        public virtual Guid? PropertyTypeId { get; set; }

        /// <summary>
        /// Schedule settings.
        /// </summary>
        /// <remarks>
        /// May contain JSON or other data.
        /// </remarks>
        public virtual string Value { get; set; }
    }
}
