using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Security;

namespace App.Data.Dto.ElectronicQueue
{
    /// <summary>
    /// Table that has data from <see cref="ScheduleSetting"/> and <seealso cref="ScheduleResource"/>
    /// </summary>
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    [NotMapped]
    public class ScheduleSettingDto: BaseDto
    {
        /// <summary>
        /// Organization to which the setting belongs.
        /// </summary>
        /// <remarks>
        /// Id of the <see cref="Organization"/>.
        /// </remarks>
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Id of the table entity for which the schedule is created.
        /// </summary>
        public Guid EntityId { get; set; }

        /// <summary>
        /// Table name.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Id of the entity to which the schedule relates.
        /// </summary>
        public Guid ResourceId { get; set; }

        /// <summary>
        /// Schedule work period <c>From</c>.
        /// </summary>
        public DateTime? WorkDateFrom { get; set; }

        /// <summary>
        /// Schedule work period <c>To</c>.
        /// </summary>
        public DateTime? WorkDateTo { get; set; }

        /// <summary>
        /// Type of schedule repeat
        /// </summary>
        /// <remarks>
        /// How often the schedule can be repeated. Use id from <see cref="EnumRecord"/> whose name is <c>ScheduleRepeat</c>.
        /// </remarks>
        public Guid ScheduleRepeatId { get; set; }

        /// <summary>
        /// Day of the week - <c>Monday</c>.
        /// </summary>
        public bool Day1 { get; set; }

        /// <summary>
        /// Day of the week - <c>Tuesday</c>.
        /// </summary>
        public bool Day2 { get; set; }

        /// <summary>
        /// Day of the week - <c>Wednesday</c>.
        /// </summary>
        public bool Day3 { get; set; }

        /// <summary>
        /// Day of the week - <c>Thursday</c>.
        /// </summary>
        public bool Day4 { get; set; }

        /// <summary>
        /// Day of the week - <c>Friday</c>.
        /// </summary>
        public bool Day5 { get; set; }

        /// <summary>
        /// Day of the week - <c>Saturday</c>.
        /// </summary>
        public bool Day6 { get; set; }

        /// <summary>
        /// Day of the week - <c>Sunday</c>.
        /// </summary>
        public bool Day7 { get; set; }

        /// <summary>
        /// Schedule is active all day.
        /// </summary>
        public bool IsFullDay { get; set; }

        /// <summary>
        /// Schedule from. Default value for time periods
        /// </summary>
        public TimeSpan WorkTimeFrom { get; set; }

        /// <summary>
        /// Schedule to. Default value for time periods
        /// </summary>
        public TimeSpan WorkTimeTo { get; set; }

        /// <summary>
        /// Schedule duration.
        /// </summary>
        public TimeSpan SlotDuration { get; set; }

        /// <summary>
        /// Break between slots.
        /// </summary>
        public TimeSpan BreakBetweenSlots { get; set; }
    }
}
