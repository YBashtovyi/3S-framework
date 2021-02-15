using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace App.Data.Models.cdn
{
    [Table("CdnWorkSubType")]
    public class WorkSubType: CoreEntity
    {
        [Required, MaxLength(50)]
        public string Code { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required, MaxLength(50), Display(Name = "Одиниця виміру")]
        public string MeasurementUnit { get; set; }

        [Required, MaxLength(50), Display(Name = "Тип класифікатору")]
        public string ClassifierType { get; set; }

        public bool IsActive { get; set; }

        public Guid? ParentId { get; set; }
    }
}
