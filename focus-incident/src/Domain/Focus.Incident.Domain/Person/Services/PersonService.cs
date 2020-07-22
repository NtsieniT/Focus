using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Domain.Report.Models;
using System.Linq;

namespace Focus.Incident.Domain.Person.Services
{
    public class PersonService
    {
        private readonly IRepository<PersonCount> repositoryPerson;

        public PersonService(IRepository<PersonCount> repositoryPerson)
        {
            this.repositoryPerson = repositoryPerson;
        }

        public IQueryable<PersonCount> Read()
        {
            return repositoryPerson.Query();
        }
    }
}
