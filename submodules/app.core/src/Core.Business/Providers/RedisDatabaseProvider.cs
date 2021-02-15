using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Core.ThirdParty.Redis
{
    public class RedisDatabaseProvider: IRedisdatabaseProvider
    {
        private ConnectionMultiplexer _redisMultiplexer;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RedisDatabaseProvider> _logger;

        public RedisDatabaseProvider(ILogger<RedisDatabaseProvider> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IDatabase GetDatabase()
        {
            if (_redisMultiplexer == null)
            {
                var url = _configuration.GetValue("ConnectionStrings:Redis", "<url not specified>");
                try
                {

                    var redisOptions = ConfigurationOptions.Parse(url);
                    redisOptions.SyncTimeout = _configuration.GetValue<int>("RedisConfiguration:ResponseTimeout");
                    _redisMultiplexer = ConnectionMultiplexer.Connect(redisOptions);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Unable to connect to Redis at address {0}", url);
                    throw;
                }

            }
            var database = _redisMultiplexer.GetDatabase();

            var minNumberOfThreads = _configuration.GetValue<int>("RedisConfiguration:MinNumberOfThreads");
            ThreadPool.GetMinThreads(out _, out var minIOC);
            ThreadPool.SetMinThreads(minNumberOfThreads, minIOC);

            return database;
        }
    }
}
