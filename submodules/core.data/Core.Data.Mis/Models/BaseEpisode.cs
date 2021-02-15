using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisEpisode")]
    public abstract class BaseEpisode: BaseEntity
    {
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual Guid PatientCardId { get; set; }
        public virtual Guid EmployeeId { get; set; }
    }
}
