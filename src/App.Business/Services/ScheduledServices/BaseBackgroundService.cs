using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace App.Business.Services.ScheduledServices
{
    /// <summary>
    /// Base class for implementing Microsoft.Extensions.Hosting.BackgroundService.
    /// </summary>
    public abstract class BaseBackgroundService: BackgroundService
    {
        /// <summary>
        /// Represents a schedule initialized from the crontab expression.
        /// </summary>
        private CrontabSchedule _crontabSchedule;

        /// <summary>
        /// Six-part expression format: 
        /// </summary>
        /// <remarks>
        /// * * * * * *
        /// - - - - - -
        /// | | | | | |
        /// | | | | | +--- day of week (0 - 6) (Sunday=0)
        /// | | | | +----- month (1 - 12)
        /// | | | +------- day of month (1 - 31)
        /// | | +--------- hour (0 - 23)
        /// | +----------- min (0 - 59)
        /// +------------- sec (0 - 59)
        /// </remarks>
        private string _cronExpression = "";

        /// <summary>
        /// Represents next occurrence of this schedule
        /// </summary>
        private DateTime _nextFireTime;

        /// <summary>
        /// Delay in miliseconds. 
        /// </summary>
        /// <remarks>
        /// Defaults = 5000 ms
        /// </remarks>
        /// 
        protected virtual int DelayMiliseconds { get; set; } = 5000;

        protected BaseBackgroundService(IConfiguration configuration, string jobName)
        {
            _cronExpression = configuration.GetValue<string>($"ScheduledProcesses:{jobName}");
            _crontabSchedule = CrontabSchedule.Parse(_cronExpression);
            _nextFireTime = _crontabSchedule.GetNextOccurrence(DateTime.UtcNow);
        }

        /// <summary>
        /// This method is called when the Microsoft.Extensions.Hosting.BackgroundService starts.
        /// </summary>
        /// <param name="stoppingToken">
        /// Triggered when Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)
        /// is called.</param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentDateTime = DateTime.UtcNow;
                if (currentDateTime > _nextFireTime)
                {
                    await ScheduledJob();
                    _nextFireTime = _crontabSchedule.GetNextOccurrence(currentDateTime);
                }
                await Task.Delay(DelayMiliseconds, stoppingToken);
            }
        }

        /// <summary>
        /// Represents a scheduled process
        /// </summary>
        /// <returns></returns>
        protected abstract Task ScheduledJob();
    }
}
