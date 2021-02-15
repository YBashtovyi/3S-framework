using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Dto.NotMappedDto
{
    /// <summary>
    /// Time period of the day
    /// </summary>
    /// <remarks>
    /// This class is used as data for other classes, for example <see cref="ScheduleTimeDto"/>
    /// </remarks>
    public class SchedulePeriodDto
    {
        /// <summary>
        /// Time period <c>From</c>
        /// </summary>
        public TimeSpan WorkTimeFrom { get; set; }

        /// <summary>
        /// Time period <c>To</c>
        /// </summary>
        public TimeSpan WorkTimeTo { get; set; }
    }
}
