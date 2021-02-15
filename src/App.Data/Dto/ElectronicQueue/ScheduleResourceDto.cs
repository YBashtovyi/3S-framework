using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.ElectronicQueue
{
    /// <summary>
    /// Contains the <c>Id</c> and <c>Name</c> of the tables for which the schedule is created.
    /// </summary>
    [MainEntity(nameof(ScheduleResource))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class ScheduleResourceDto: BaseDto
    {
        /// <summary>
        /// Id of the table entity for which the schedule is created.
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid EntityId { get; set; }

        /// <summary>
        /// Table name.
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string EntityName { get; set; }

        /// <summary>
        /// Organization to which the resource belongs.
        /// </summary>
        /// <remarks>
        /// Id of the <see cref="Organization"/>.
        /// </remarks>
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid OrganizationId { get; set; }
    }
}
