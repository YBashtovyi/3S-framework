using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Models
{
    [Table("SysAuditHistory")]
    public class AuditHistory: IEntity
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public RecordState RecordState { get; set; }
        public string Entity { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}
