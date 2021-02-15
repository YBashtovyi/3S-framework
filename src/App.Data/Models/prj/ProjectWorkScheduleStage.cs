using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace App.Data.Models
{
    [Table("PrjWorkScheduleStage")]
    public class ProjectWorkScheduleStage: CoreEntity
    {
        [Required, Display(Name = "До документу")]
        public Guid PrjWorkScheduleId { get; set; }

        [Required, MaxLength(50), Display(Name = "№ етапу")]
        public string StageNumber { get; set; }

        [Required, MaxLength(200)]
        public string StageName { get; set; }

        [Required, Display(Name = "Дата початку")]
        public DateTime BeginDate { get; set; }

        [Required, Display(Name = "Дата закінчення")]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Cost { get; set; }

        public Document PrjWorkSchedule { get; set; }
    }
}
