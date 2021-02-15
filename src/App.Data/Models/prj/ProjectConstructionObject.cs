using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using App.Data.Models;
using Core.Base.Data;

namespace App.Data.Models
{
    [Table("PrjConstructionObject")]
    public class ProjectConstructionObject: CoreEntity
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid ConstructionObjectId { get; set; }

        public Project Project { get; set; }
        public ConstructionObject ConstructionObject { get; set; }
    }
}
