using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Relation between eHealth contracts and eHealth divisions
    /// </summary>
    [Display(Name = "Підрозділ в контракті")]
    [Table("EhdContractDivison")]
    public abstract class BaseEhealthContractDivison: BaseEntity
    {
        public virtual Guid DivisionId { get; set; }
        public virtual Guid EntityId { get; set; }
    }
}
