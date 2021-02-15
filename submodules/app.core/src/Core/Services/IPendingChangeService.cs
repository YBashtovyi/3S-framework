using System;
using Core.Models;

namespace Core.Services
{
    public interface IPendingChangeService
    {
        PendingChange Create(string entityName, Guid entityId, string operationType);

        /// <summary>
        /// The method used for disabling creation Pending Change entities, for disabling creating Pending Change entities - pass true as a parameter, for enabling creating - pass false
        /// </summary>
        /// <param name="value">bool value</param>
        void Disable(bool value);
    }
}

