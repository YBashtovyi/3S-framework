using System;

namespace Core.Services.CorrelationId
{
    /// <summary>
    /// Provides a correlation ID.
    /// </summary>
    public interface ICorrelationIdProvider
    {
        /// <summary>
        /// Creates a correlation ID.
        /// </summary>
        /// <returns>A correlation ID.</returns>
        string GenerateCorrelationId();
    }
}
