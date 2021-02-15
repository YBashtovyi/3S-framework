using System;
using System.Collections.Generic;
using System.Text;
using Core.Data.Eq.Models;
using Core.Security;

namespace App.Data.Models
{
    /// <inheritdoc />
    [MainEntity(nameof(ScheduleTime))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class ScheduleTime : BaseScheduleTime
    {
        /// <summary>
        /// Day of the week.
        /// </summary>
        /// <remarks>
        /// You can select Monday, Tuesday or another day. Use the <c>EnumType</c> that has the name - <c>DayOfWeek</c>
        /// </remarks>
        public virtual EnumRecord DayOfWeek { get; set; }

        public virtual Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
