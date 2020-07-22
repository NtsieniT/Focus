using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Domain.Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Focus.Incident.Domain.Report.Services
{
    public class SummaryService
    {
        private readonly IRepository<SummaryCounters> repositorySummary;

        public SummaryService(IRepository<SummaryCounters> repositoryPrimaryBusinessLine)
        {
            this.repositorySummary = repositoryPrimaryBusinessLine;
        }

        public IQueryable<SummaryCounters> Read()
        {
            return repositorySummary.Query();
        }
    }
}
