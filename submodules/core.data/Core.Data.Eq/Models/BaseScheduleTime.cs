using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Eq.Models
{
    /// <summary>
    /// Schedule table where working hours are saved.
    /// </summary>
    [Table("EqScheduleTime")]
    public abstract class BaseScheduleTime: CoreEntity
    {
        /// <summary>
        /// Id of the <see cref="BaseScheduleSetting"/>.
        /// </summary>
        public virtual Guid ScheduleSettingId { get; set; }

        /// <summary>
        /// Day of the week.
        /// </summary>
        /// <remarks>
        /// You can select Monday, Tuesday or another day. For example, you can use <see cref="BaseEnumRecord"/> whose name is - <c>DayOfWeek</c>
        /// </remarks>
        public virtual Guid DayOfWeekId { get; set; }

        /// <summary>
        /// It allows you to specify the weekend on the current day.
        /// </summary>
        public virtual bool IsWeekend { get; set; }

        /// <summary>
        /// Specific date for the slot.
        /// </summary>
        public virtual DateTime ScheduleDate { get; set; }

        /// <summary>
        /// Time period <c>From</c>.
        /// </summary>
        public virtual TimeSpan WorkTimeFrom { get; set; }

        /// <summary>
        /// Time period <c>To</c>.
        /// </summary>
        public virtual TimeSpan WorkTimeTo { get; set; }

        /// <summary>
        /// Working hours.
        /// </summary>
        public virtual TimeSpan WorkTimeDuration { get; set; }

        /// <summary>
        /// Break between slots.
        /// </summary>
        public virtual TimeSpan BreakBetweenSlots { get; set; }
    }
}
