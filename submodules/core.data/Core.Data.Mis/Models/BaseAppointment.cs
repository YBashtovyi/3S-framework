using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    /// <summary>
    /// Entity that repsents the Appointment
    /// </summary>
    [Table("MisAppointment")]
    public abstract class BaseAppointment : BaseEntity
    {

        /// <summary>
        /// Description(comment) where additional data can be stored
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Id which relates to the department where appointment was created
        /// </summary>
        public virtual Guid? DepartmentId { get; set; }

        /// <summary>
        /// Id of employee that moderates the appointment
        /// </summary>
        public virtual Guid EmployeeId { get; set; }

        /// <summary>
        /// Id of the business logic state of the appointment
        /// </summary>
        public virtual Guid StateId { get; set; }

        /// <summary>
        /// Id of the appointment visit type that resresents the 
        /// way how patient was arived 
        /// </summary>
        public virtual Guid? VisitTypeId { get; set; }

        /// <summary>
        /// Id of the organization where appointment was created
        /// </summary>
        public virtual Guid OrganizationId { get; set; }

        /// <summary>
        /// Id of the patient card which relates to the appointment
        /// </summary>
        public virtual Guid PatientCardId { get; set; }

        /// <summary>
        /// Start date of the appointment when appointment was started for read
        /// </summary>
        public virtual DateTime? StartDateFact { get; set; }

        /// <summary>
        /// End date of the appointment when appointment was ended for read
        /// </summary>
        public virtual DateTime? EndDateFact { get; set; }
        
        /// <summary>
        /// Plan start date of the appointment
        /// </summary>
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// Plan end date of the appointment
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

    }
}
