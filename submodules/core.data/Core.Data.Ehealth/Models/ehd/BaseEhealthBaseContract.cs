using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Table("EhdBaseContract")]
    public abstract class BaseEhealthBaseContract: BaseEntity
    {
        public virtual string Number { get; set; }
        public virtual DateTime IssuedAt { get; set; }
        public virtual DateTime ExpiresAt { get; set; }
        public virtual Guid EntityId { get; set; }
    }
}
