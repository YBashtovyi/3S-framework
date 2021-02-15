using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using App.Data.Models.cdn;
using Core.Base.Data;

namespace App.Data.Models
{
    [Table("PrjWorkScheduleSubType")]
    public class ProjectWorkScheduleSubType: CoreEntity
    {
        [Required, Display(Name = "До документу")]
        public Guid PrjWorkScheduleId { get; set; }

        [Required]
        public Guid PrjWorkScheduleStageId { get; set; }

        [Required]
        public Guid WorkSubTypeId { get; set; }

        [Required, MaxLength(50), Display(Name = "Одиниця виміру")]
        public string MeasurementUnit { get; set; }

        [Display(Name = "Кількість одиниць")]
        public float Amount { get; set; }

        [Display(Name = "Цільовий показник")]
        public float Target { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public Document PrjWorkSchedule { get; set; }
        public ProjectWorkScheduleStage PrjWorkScheduleStage { get; set; }
        public WorkSubType WorkSubType { get; set; }
    }
}
