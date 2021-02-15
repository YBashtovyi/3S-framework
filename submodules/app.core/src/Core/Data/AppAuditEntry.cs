using Core.Base.Data;
using Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Core.Data
{
    public class AppAuditEntry
    {
        public AppAuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string Entity { get; set; }
        public Guid AuthorId { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public bool HasModifications => OldValues.Any() || NewValues.Any();

        public AuditHistory ToAudit()
        {
            var audit = new AuditHistory
            {
                Id = Guid.NewGuid(),
                RecordState = RecordState.Active,
                Entity = Entity,
                AuthorId = AuthorId,
                DateTime = DateTime.UtcNow,
                KeyValues = JsonSerializer.Serialize(KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues)
            };
            return audit;
        }
    }
}
