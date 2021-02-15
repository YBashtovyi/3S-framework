using System;
using Core.Data.Eq.Models;
using Core.Security;

namespace App.Data.Models
{
    /// <inheritdoc />
    [MainEntity(nameof(ScheduleSlot))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class ScheduleSlot : BaseScheduleSlot
    {
        /// <summary>
        /// Type of the slot
        /// </summary>
        /// <remarks>
        /// Used for additional analytics. Use the <c>EnumType</c> that has the name - <c>ScheduleTypeSlot</c>.
        /// </remarks>
        public virtual EnumRecord SlotType { get; set; }

        /// <summary>
        /// State of the slot
        /// </summary>
        /// <remarks>
        /// The ability to indicate the status of a slot, for example, open or closed. Use the <c>EnumType</c> that has the name - <c>SlotState</c>.
        /// </remarks>
        public virtual EnumRecord SlotState { get; set; }

        public virtual Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
