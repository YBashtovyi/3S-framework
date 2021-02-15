using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisEvent")]
    public abstract class BaseEvent: BaseEntity, IEvent
    {
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual string Description { get; set; }
        public string RegNumber { get; set; }
        public DateTime RegDate { get; set; }
        public Guid PatientCardId { get; set; }
        public Guid EmployeeId { get; set; }
        
    }
}
