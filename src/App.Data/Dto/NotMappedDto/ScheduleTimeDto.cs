using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Dto.NotMappedDto
{
    /// <summary>
    /// Day of the week with periods
    /// </summary>
    /// <remarks>
    /// This class is used as data for other classes, for example <see cref="ScheduleDivisionDto"/>
    /// </remarks>
    public class ScheduleTimeDto
    {
        /// <summary>
        /// Weekday name
        /// </summary>
        public string DayOfWeek { get; set; }

        /// <summary>
        /// Weekday enum (code)
        /// </summary>
        public string DayOfWeekEnum { get; set; }

        /// <summary>
        /// Weekday number
        /// </summary>
        public int DayOfWeekNum { get; set; }

        /// <summary>
        /// Contains all time periods for the current day of the week.
        /// </summary>
        public List<SchedulePeriodDto> Periods { get; set; } = new List<SchedulePeriodDto>();
    }
}
