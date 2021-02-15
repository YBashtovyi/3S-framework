using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Eq.Models
{
    /// <summary>
    /// Table where settings for primary schedule generation are stored.
    /// </summary>
    [Table("EqScheduleSetting")]
    public abstract class BaseScheduleSetting: CoreEntity
    {
        /// <summary>
        /// Id of the entity to which the schedule relates.
        /// </summary>
        public virtual Guid ResourceId { get; set; }

        /// <summary>
        /// Schedule work period <c>From</c>.
        /// </summary>
        public virtual DateTime? WorkDateFrom { get; set; }

        /// <summary>
        /// Schedule work period <c>To</c>.
        /// </summary>
        public virtual DateTime? WorkDateTo { get; set; }

        /// <summary>
        /// Type of schedule repeat
        /// </summary>
        /// <remarks>
        /// How often the schedule can be repeated. For example, you can use <see cref="BaseEnumRecord"/> whose name is <c>ScheduleRepeat</c>.
        /// </remarks>
        public virtual Guid ScheduleRepeatId { get; set; }

        /// <summary>
        /// Day of the week - <c>Monday</c>.
        /// </summary>
        public virtual bool Day1 { get; set; }

        /// <summary>
        /// Day of the week - <c>Tuesday</c>.
        /// </summary>
        public virtual bool Day2 { get; set; }

        /// <summary>
        /// Day of the week - <c>Wednesday</c>.
        /// </summary>
        public virtual bool Day3 { get; set; }

        /// <summary>
        /// Day of the week - <c>Thursday</c>.
        /// </summary>
        public virtual bool Day4 { get; set; }

        /// <summary>
        /// Day of the week - <c>Friday</c>.
        /// </summary>
        public virtual bool Day5 { get; set; }

        /// <summary>
        /// Day of the week - <c>Saturday</c>.
        /// </summary>
        public virtual bool Day6 { get; set; }

        /// <summary>
        /// Day of the week - <c>Sunday</c>.
        /// </summary>
        public virtual bool Day7 { get; set; }

        /// <summary>
        /// Schedule is active all day.
        /// </summary>
        public virtual bool IsFullDay { get; set; }

        /// <summary>
        /// Schedule from. Default value for time periods
        /// </summary>
        public virtual TimeSpan WorkTimeFrom { get; set; }

        /// <summary>
        /// Schedule to. Default value for time periods
        /// </summary>
        public virtual TimeSpan WorkTimeTo { get; set; }

        /// <summary>
        /// Schedule duration.
        /// </summary>
        public virtual TimeSpan SlotDuration { get; set; }

        /// <summary>
        /// Break between slots.
        /// </summary>
        public virtual TimeSpan BreakBetweenSlots { get; set; }
    }
}
