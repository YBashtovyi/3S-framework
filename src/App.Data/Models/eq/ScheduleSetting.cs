using System;
using Core.Data.Eq.Models;
using Core.Security;

namespace App.Data.Models
{
    /// <inheritdoc />
    [MainEntity(nameof(ScheduleSetting))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class ScheduleSetting : BaseScheduleSetting
    {
        /// <summary>
        /// Type of schedule repeat
        /// </summary>
        /// <remarks>
        /// How often the schedule can be repeated. Use the <c>EnumType</c> that has the name <c>ScheduleRepeat</c>.
        /// </remarks>
        public virtual EnumRecord ScheduleRepeat { get; set; }

        public virtual Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
