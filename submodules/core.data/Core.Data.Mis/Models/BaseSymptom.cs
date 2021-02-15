using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisSymptom")]
    public abstract class BaseSymptom: BaseEntity
    {
        public string CaptionRu { get; set; }
        public Guid? IcpcId { get; set; }
    }
}
