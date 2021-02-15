using System;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Core.Services.Data
{
    public class PendingChangeService: IPendingChangeService
    {
        private readonly IConfiguration _configuration;
        private bool IsDisabled;

        public PendingChangeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Disable(bool value)
        {
            IsDisabled = value;
        }
        public PendingChange Create(string entityName, Guid entityId, string operationType)
        {
            if (Validate(entityName, entityId, operationType))
            {
                return new PendingChange()
                {
                    Action = operationType,
                    EntityName = entityName,
                    EntityId = entityId,
                    IsError = false
                };
            }

            return null;
        }

        private bool Validate(string entityName, Guid entityId, string operationType)
        {
            if (IsDisabled)
            {
                return false;
            } 

            var success = false;
            if (!string.IsNullOrWhiteSpace(entityName) && entityId != Guid.Empty && !string.IsNullOrWhiteSpace(operationType))
            {
                success = _configuration.GetValue<bool>($"Synchronization:Entities:{entityName}:Send");
            }

            return success;
        }
    }
}
