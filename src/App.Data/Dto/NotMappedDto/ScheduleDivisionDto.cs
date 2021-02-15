using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Dto.NotMappedDto
{
    /// <summary>
    /// Used to display the division schedule.
    /// </summary>
    public class ScheduleDivisionDto
    {
        /// <summary>
        /// Id from the schedule settings table.
        /// </summary>
        public Guid ScheduleSettingId { get; set; }

        /// <summary>
        /// Contains all time periods of the division schedule.
        /// </summary>
        public List<ScheduleTimeDto> ScheduleTimes { get; set; } = new List<ScheduleTimeDto>();
    }
}
