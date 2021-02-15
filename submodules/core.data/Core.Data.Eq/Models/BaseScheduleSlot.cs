using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Eq.Models
{
    /// <summary>
    /// The time slot for making an appointment.
    /// </summary>
    [Table("EqScheduleSlot")]
    public abstract class BaseScheduleSlot: CoreEntity
    {
        /// <summary>
        /// Id from the schedule settings table.
        /// </summary>
        public virtual Guid? ScheduleSettingId { get; set; }

        /// <summary>
        /// Defines the date to which the schedule applies.
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Time period <c>From</c>.
        /// </summary>
        public virtual TimeSpan TimeFrom { get; set; }

        /// <summary>
        /// Time period <c>To</c>.
        /// </summary>
        public virtual TimeSpan TimeTo { get; set; }

        /// <summary>
        /// Type of the slot
        /// </summary>
        /// <remarks>
        /// Used for additional analytics. For example, you can use <see cref="BaseEnumRecord"/> whose name is - <c>ScheduleTypeSlot</c>.
        /// </remarks>
        public virtual Guid? SlotTypeId { get; set; }

        /// <summary>
        /// State of the slot
        /// </summary>
        /// <remarks>
        /// The ability to indicate the status of a slot, for example, open or closed. For example, you can use <see cref="BaseEnumRecord"/> whose name is - <c>SlotState</c>.
        /// </remarks>
        public virtual Guid? SlotStateId { get; set; }
    }
}
