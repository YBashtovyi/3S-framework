using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services;
using Prometheus;

namespace Core.Business.Services
{
    public class MetricReporter
    {
        private readonly IUserInfoService _userInfoService;
        private readonly Histogram _responseTimeHistogram;
        private readonly List<string> _sqlElapsed = new List<string>();

        public MetricReporter(IUserInfoService UserInfoService)
        {
            _userInfoService = UserInfoService;
            _responseTimeHistogram = Metrics.CreateHistogram("request_counter", "test", new HistogramConfiguration
            {
                Buckets = Histogram.LinearBuckets(0, 0.1, 5),
                LabelNames = new[] { "method", "response", "url", "queryString", "sql", "personFullName", "organizationName", }
            });
        }

        public void RegisterResponseTime(List<string> vals, TimeSpan elapsed)
        {
            vals.Add(!_sqlElapsed.Any() ? "" : _sqlElapsed.Aggregate((a, b) => $"{a} | {b}"));

            var currentUser = _userInfoService.GetCurrentUserInfo();
            if (currentUser.LoginData != null)
            {
                currentUser.LoginData.TryGetValue("personFullName", out var personFullName);
                currentUser.LoginData.TryGetValue("organizationName", out var organizationName);
                vals.Add(personFullName ?? string.Empty);
                vals.Add(organizationName ?? string.Empty);
            }
            else
            {
                vals.AddRange(new[] { string.Empty, string.Empty });
            }

            _responseTimeHistogram.WithLabels(vals.ToArray()).Observe(elapsed.TotalSeconds);
            _sqlElapsed.Clear();
        }

        public void RegisterSqlTime(string dto, TimeSpan elapsed)
        {
            _sqlElapsed.Add($"{dto}-{elapsed.TotalSeconds}");
        }
    }
}
