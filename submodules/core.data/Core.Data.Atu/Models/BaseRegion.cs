using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Atu.Models
{
    [Display(Name = "Довідник областей")]
    [Table("AtuRegion")]
    public abstract class BaseRegion : BaseEntity
    {
        [Display(Name = "Підпорядкування")]
        public virtual Guid? ParentId { get; set; }

        [MaxLength(64)]
        public virtual string Code { get; set; }

        public virtual Guid CountryId { get; set; }

        [MaxLength(15)]
        [Display(Name = "КОАТУУ")]
        public virtual string KOATUU { get; set; }
    }
}
