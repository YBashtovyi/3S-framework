using System;
using System.Collections.Generic;
using Core.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Services
{
    public interface IAuditEntityEntryChangesTracker
    {
        IEnumerable<AppAuditEntry> GetChanges(IEnumerable<EntityEntry> entityEntries, Guid authorId);
    }
}