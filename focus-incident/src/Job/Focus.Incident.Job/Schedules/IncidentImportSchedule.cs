using Focus.Incident.Domain.Incident.Service;
using Focus.Incident.Job.Scheduling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Incident.Job.Schedules
{
    public class IncidentImportSchedule : IScheduledTask
    {
        private bool isRunning;
        private const string jobName = "Incident Import";
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<IncidentImportSchedule> logger;

        public IncidentImportSchedule(IServiceScopeFactory serviceScopeFactory, ILogger<IncidentImportSchedule> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }

        public string Schedule { get; set; }

        public async Task ExecuteAsync(CancellationToken cancellationToken, int interval)
        { 
            if (isRunning) return;

            logger.LogInformation($"Started '{jobName}' {interval} Minutes job.");
            isRunning = true;
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IncidentImportService>();
                    await service.Sync();
                } 
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, $"Error in '{jobName}' job.");
                logger.LogError("Error in '{0}' job: {1}", jobName, ex);
            }

            isRunning = false;
            logger.LogInformation($"Ended '{jobName}' {interval} Minutes job.");
        }
    }
}
