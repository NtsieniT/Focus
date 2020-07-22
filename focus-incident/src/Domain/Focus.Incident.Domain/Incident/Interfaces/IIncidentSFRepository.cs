using Focus.Incident.Domain.Incident.Models.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Incident.Domain.Incident.Interfaces
{
    public interface IIncidentSFRepository
    {
        //IEnumerable<Incident.Models.Incident> Read();
        Task<IncidentQueryResult> ReadAsync(IncidentQuery query);
    }
}
