using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Core.Services.CorrelationId
{
    public class DefaultCorrelationIdProvider: ICorrelationIdProvider
    {
        private readonly CorrelationIdOptions _options;
        private readonly IHttpContextAccessor _contextAccessor;

        public DefaultCorrelationIdProvider(IHttpContextAccessor contextAccessor, IOptions<CorrelationIdOptions> options) 
        {
            _contextAccessor = contextAccessor;
            _options = options.Value;
        }

        public string GenerateCorrelationId()
        {
            var httpContext = _contextAccessor?.HttpContext;
            if (httpContext == null)
            {
                return GenerateCorrelationId(null);
            }

            httpContext.Request.Headers.TryGetValue(_options.Header, out var correlationId);
            if (StringValues.IsNullOrEmpty(correlationId))
            {
                correlationId = GenerateCorrelationId(httpContext?.TraceIdentifier);
            }

            return correlationId;
        }

        private StringValues GenerateCorrelationId(string traceIdentifier) =>
            _options.UseGuidForCorrelationId || string.IsNullOrEmpty(traceIdentifier) ? Guid.NewGuid().ToString() : traceIdentifier;
    }
}
