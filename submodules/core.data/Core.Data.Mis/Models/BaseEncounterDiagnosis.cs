using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisEncounterDiagnosis")]
    public abstract class BaseEncounterDiagnosis: BaseEntity
    {
        public Guid EncounterId { get; set; }
    }
}
