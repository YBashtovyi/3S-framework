using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace App.Data.Models
{
    [Table("PrjWorkSchedule")]
    public class ProjectWorkSchedule: CoreEntity, IDerivedEntity
    {
        #region IDerivedEntity
        public Type BaseType => typeof(Document);
        #endregion

        [Required]
        public Guid ProjectId { get; set; }

        [Required, MaxLength(50)]
        public string DocType { get; set; }

        public Guid? ParentId { get; set; }

        public DateTime RegDate { get; set; }

        [Required, MaxLength(100)]
        public string RegNumber { get; set; }

        [Required, MaxLength(50)]
        public string DocState { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public Document Parent { get; set; }

        public Project Project { get; set; }
    }
}
