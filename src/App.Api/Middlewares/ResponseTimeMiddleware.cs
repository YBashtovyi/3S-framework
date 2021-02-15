using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace App.WebAPI.Middlewares
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _request;

        public ResponseTimeMiddleware(
            RequestDelegate request
        )
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task InvokeAsync(HttpContext httpContext, MetricReporter reporter)
        {
            var path = httpContext.Request.Path.Value;
            if (path == "/metrics")
            {
                await _request.Invoke(httpContext);
                return;
            }
            var sw = Stopwatch.StartNew();
            List<string> vals = new List<string>();

            vals.Add(httpContext.Request.Method);
            vals.Add(httpContext.Response.StatusCode.ToString());
            vals.Add(httpContext.Request.Path.Value);
            vals.Add(httpContext.Request.QueryString.Value);

            try
            {
                await _request.Invoke(httpContext);
            }
            finally
            {
                sw.Stop();
                //reporter.RegisterResponseTime(vals, sw.Elapsed);
            }
        }
    }

    public static class PrometheusMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseTimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseTimeMiddleware>();
        }
    }
}
