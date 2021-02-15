using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Sinks.Loki.Labels;

namespace App.Api.Logging
{
    public class LogLabelProvider : ILogLabelProvider
    {
        public IList<LokiLabel> GetLabels()
        {
            return new List<LokiLabel>
            {
                new LokiLabel(key : "app", value : "Marvel"),
                new LokiLabel(key : "environment", value : $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}")
            };
        }
    }
}
