using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisSymptomLocation")]
    public abstract class BaseSymptomLocation: BaseEntity
    {
        public Guid? ParentId { get; set; }
    }
}
