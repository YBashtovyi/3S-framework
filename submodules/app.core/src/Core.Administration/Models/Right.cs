using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Administration.Models
{
    [Table("Adm" + nameof(Right))]
    public class Right: CoreEntity
    {
        [Required, MaxLength(50)]
        public string RightType { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Code { get; set; }

        [Required, MaxLength(250)]
        public string Description { get; set; }

        [Column(TypeName = "json")]
        public string Els { get; set; }
    }
}
