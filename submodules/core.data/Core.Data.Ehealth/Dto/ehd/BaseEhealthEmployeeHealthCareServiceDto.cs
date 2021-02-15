using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents services that employees provide
    /// </summary>
    public abstract class BaseEhealthEmployeeHealthCareServiceDto: BaseDto
    {
        /// <summary>
        /// Reference to entity that represents employee in ehealth
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        [RequiredNonDefault]
        public virtual Guid EmployeeId { get; set; }

        /// <summary>
        /// Reference to entity that represents available health care services in eHealth
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        [RequiredNonDefault]
        public virtual Guid HealthCareServiceId { get; set; }

        /// <summary>
        /// Id of entity in eHealth
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? EhealthId { get; set; }

        /// <summary>
        /// Reference to entity that represents statuses in eHealth
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        [RequiredNonDefault]
        public virtual Guid StatusId { get; set; }

        /// <summary>
        /// Date, when this record was activated in eHealth
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// Date, when this record was deactivated in eHealth
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime? EndDate { get; set; }
    }
}
