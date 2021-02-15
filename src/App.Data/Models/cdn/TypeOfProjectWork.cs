using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace App.Data.Models
{
    [Table("CdnTypeOfProjectWork")]
    public class TypeOfProjectWork: CoreEntity
    {
        [Required, MaxLength(50)]
        public string Code { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required, MaxLength(300)]
        public string FullName { get; set; }

        public Guid? ParentId { get; set; }
    }
}
