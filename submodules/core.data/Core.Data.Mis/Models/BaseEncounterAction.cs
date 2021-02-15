﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Display(Name = "Взаємодії: дії")]
    [Table("MisEncounterAction")]
    public abstract class BaseEncounterAction: BaseEntity
    {
        public Guid EncounterId { get; set; }
    }
}