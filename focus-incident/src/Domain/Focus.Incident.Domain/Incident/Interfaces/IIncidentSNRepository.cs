using Focus.Incident.Domain.Incident.Models.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Incident.Domain.Incident.Interfaces
{
    public interface IIncidentSNRepository
    {
        //IEnumerable<Models.Incident> Read();
        System.Threading.Tasks.Task<IncidentQueryResult> ReadAsync(IncidentQuery query);
    }
}
