using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisConsultation")]
    public abstract class BaseConsultation: BaseDocument
    {
        public virtual Guid EmployeeId { get; set; }
    }
}
