using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Domain.Incident.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Focus.Incident.Domain.Incident.Service
{
    public class IncidentService
    {
        private readonly IRepository<Models.Incident> repositoryIncident;

        public IncidentService(IRepository<Models.Incident> repositoryIncident)
        {
            this.repositoryIncident = repositoryIncident;
        }

        public IQueryable<Models.Incident> Read()
        {
            return repositoryIncident.Query();
        }
    }
}
