using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Domain.Report.Models;
using System.Linq;

namespace Focus.Incident.Domain.PrimaryBusinessLine.Services
{
   public class PrimaryBusinessLineService
    {
        private readonly IRepository<BusinessLineCount> repositoryPrimaryBusinessLine;

        public PrimaryBusinessLineService(IRepository<BusinessLineCount> repositoryPrimaryBusinessLine)
        {
            this.repositoryPrimaryBusinessLine = repositoryPrimaryBusinessLine;
        }
        public IQueryable<BusinessLineCount> Read()
        {
            return repositoryPrimaryBusinessLine.Query().Where(x => x.BusinessLine.Contains("AFRICA"));
        }
    }
}
