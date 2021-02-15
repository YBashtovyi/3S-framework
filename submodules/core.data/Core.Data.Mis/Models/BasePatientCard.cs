using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisPatientCard")]
    public abstract class BasePatientCard: BaseDocument
    {
        public virtual Guid PersonId { get; set; }
    }
}
