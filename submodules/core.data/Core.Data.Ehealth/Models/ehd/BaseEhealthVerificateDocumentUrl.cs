using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Table("EhdVerificateDocumentUrl")]
    public abstract class BaseEhealthVerificateDocumentUrl: BaseEntity
    {
        public virtual string Url { get; set; }
        public virtual string Type { get; set; }
        public virtual Guid EntityId { get; set; }
        public virtual bool IsUploaded { get; set; }
    }
}
