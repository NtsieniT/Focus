using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Incident.Job.Scheduling
{
    public interface IScheduledTask
    {
        string Schedule { get; set; }
        Task ExecuteAsync(CancellationToken cancellationToken, int interval);
    }
}
