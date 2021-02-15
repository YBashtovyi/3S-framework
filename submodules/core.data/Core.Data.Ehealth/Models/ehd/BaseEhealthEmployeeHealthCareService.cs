using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents services that employees provide
    /// </summary>
    public abstract class BaseEhealthEmployeeHealthCareService: BaseEntity
    {
        /// <summary>
        /// Reference to entity that represents employee in ehealth
        /// </summary>
        public virtual Guid EmployeeId { get; set; }

        /// <summary>
        /// Reference to entity that represents available health care services in eHealth
        /// </summary>
        public virtual Guid HealthCareServiceId { get; set; }

        /// <summary>
        /// Id of entity in eHealth
        /// </summary>
        public virtual Guid? EhealthId { get; set; }

        /// <summary>
        /// Reference to entity that represents statuses in eHealth
        /// </summary>
        public virtual Guid StatusId { get; set; }

        /// <summary>
        /// Date, when this record was activated in eHealth
        /// </summary>
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// Date, when this record was deactivated in eHealth
        /// </summary>
        public virtual DateTime? EndDate { get; set; }
    }
}
