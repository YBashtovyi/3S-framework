using System;
using Core.Data.Eq.Models;
using Core.Security;

namespace App.Data.Models
{
    /// <inheritdoc />
    [MainEntity(nameof(ScheduleSettingProperty))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class ScheduleSettingProperty : BaseScheduleSettingProperty
    {
        /// <summary>
        /// Type of schedule specific properties.
        /// </summary>
        /// <remarks>
        /// Specific properties that are not added to the schedule settings table. Use the <c>EnumType</c> that has the name - <c>SchedulePropertyType</c>.
        /// </remarks>
        public virtual EnumRecord PropertyType { get; set; }

        public virtual Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
