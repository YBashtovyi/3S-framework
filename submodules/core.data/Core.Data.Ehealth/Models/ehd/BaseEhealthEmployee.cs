using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// eHealth employee
    /// </summary>
    [Display(Name = "Співробітник eHealth")]
    [Table("EhdEmployee")]
    public abstract class BaseEhealthEmployee: BaseEntity
    {
        public virtual Guid? DivisionId { get; set; }
        public virtual Guid? LegalEntityId { get; set; }
        public virtual string PositionCode { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string EmployeeTypeCode { get; set; }

        /// <summary>
        /// Id in Ehealth. 
        /// Is set in eHealth when an employee is registered there
        /// </summary>
        public virtual Guid? EhealthId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid? EhealthRequestId { get; set; }
    }
}
