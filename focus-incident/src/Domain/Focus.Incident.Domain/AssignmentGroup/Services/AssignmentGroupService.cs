using Focus.Incident.Domain.AssignmentGroup.Models;
using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Domain.Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Focus.Incident.Domain.AssignmentGroup.Services
{
    public class AssignmentGroupService
    {
        private readonly IRepository<AssignmentGroupCount> repositoryApplicationGroup;

        public AssignmentGroupService(IRepository<AssignmentGroupCount> repositoryApplicationGroup)
        {
            this.repositoryApplicationGroup = repositoryApplicationGroup;
        }

        public IQueryable<AssignmentGroupCount> Read()
        {
            return repositoryApplicationGroup.Query();
        }
    }
}
