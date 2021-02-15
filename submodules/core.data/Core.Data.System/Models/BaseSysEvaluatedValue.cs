using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.System.Models
{
    [Table("SysEvaluatedValue")]
    public abstract class BaseSysEvaluatedValue: BaseEntity
    {
        [Required]
        public virtual string EntityName { get; set; }
        [Required]
        public virtual Guid EntityId { get; set; }
        [Required]
        public virtual string ValueName { get; set; }
        [Required]
        public virtual string ValueType { get; set; }
        public virtual string Value { get; set; }
    }
}
