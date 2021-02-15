using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Cdn.Models
{
    [Display(Name = "Класифікатор ICD-10")]
    [Table("CdnIcd10")]
    public abstract class BaseIcd10: BaseDirectory
    {
        public virtual Guid? ParentId { get; set; }
        public string NameUa { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
    }
}
