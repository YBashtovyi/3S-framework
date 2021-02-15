using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Models
{
    [Table("CmnPendingChange")]
    public class PendingChange : CoreEntity
    {
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public string Owner { get; set; }
        public string Action { get; set; }
        public bool Processed { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

    }
}

