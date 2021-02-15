using System;
using Core.Data.Eq.Models;
using Core.Security;

namespace App.Data.Models
{
    /// <inheritdoc />
    [MainEntity(nameof(ScheduleResource))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class ScheduleResource : BaseScheduleResource
    {
        public virtual Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
