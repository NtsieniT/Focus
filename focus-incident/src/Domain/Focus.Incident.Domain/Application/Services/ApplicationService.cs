using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Domain.Report.Models;
using System.Linq;

namespace Focus.Incident.Domain.Application.Services
{
    public class ApplicationService
    {
        private readonly IRepository<ApplicationCount> repositoryApplication;

        public ApplicationService(IRepository<ApplicationCount> repositoryApplication)
        {
            this.repositoryApplication = repositoryApplication;
        }

        public IQueryable<ApplicationCount> Read()
        {
            return repositoryApplication.Query();
        }
    }
}
