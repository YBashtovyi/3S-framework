using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Core.Models
{
    public interface IAuditableContext
    {
        int SaveAuditable(BaseUserInfo user);
        int SaveAuditable(BaseUserInfo user, bool acceptAllChangesOnSuccess);
        Task<int> SaveAuditableAsync(BaseUserInfo user, CancellationToken cancellationToken = default);
        Task<int> SaveAuditableAsync(BaseUserInfo user, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
