using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Display(Name = "Взаємодії: причини звернення")]
    [Table("MisEncounterReason")]
    public abstract class BaseEncounterReason: BaseEntity
    {
        public Guid EncounterId { get; set; }
    }
}
