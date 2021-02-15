using Core.Services.CorrelationId;
using Serilog.Core;
using Serilog.Events;

namespace App.Api.Logging
{
    public class CorrelationIdEnricher: ILogEventEnricher
    {
        private readonly ICorrelationContextAccessor _contextAccessor;

        public CorrelationIdEnricher() : this(new CorrelationContextAccessor())
        {
        }

        public CorrelationIdEnricher(ICorrelationContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_contextAccessor?.CorrelationContext?.CorrelationId == null)
            {
                return;
            }

            var correlationIdProperty = propertyFactory.CreateProperty(CorrelationIdOptions.DefaultHeader, _contextAccessor?.CorrelationContext?.CorrelationId);
            logEvent.AddPropertyIfAbsent(correlationIdProperty);
        }
    }
}
