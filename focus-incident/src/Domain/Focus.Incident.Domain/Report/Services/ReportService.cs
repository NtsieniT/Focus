using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Domain.Report.Interfaces;
using Focus.Incident.Domain.Report.Models;
using System.Linq;
using DomainModel = Focus.Incident.Domain.Incident.Models;

namespace Focus.Incident.Domain.Report.Services
{
    //Reads and gets count from db and displays on screen
    public class ReportService
    {

        private readonly IReportRepository<ApplicationCount> repositoryApplicationCount;
        private readonly IReportRepository<AssignmentGroupCount> repositorAssignmentGroupCount;
        private readonly IReportRepository<BusinessLineCount> repositoryPrimaryBusinessLineCount;
        private readonly IReportRepository<PersonCount> repositoryPersonCount;
        private readonly IReportRepository<SummaryCounters> repositorySummaryCount;

        public ReportService(IReportRepository<ApplicationCount> repositoryApplicationCount, IReportRepository<AssignmentGroupCount> repositorAssignmentGroupCount, IReportRepository<BusinessLineCount> repositoryPrimaryBusinessLineCount, IReportRepository<PersonCount> repositoryPersonCount, IReportRepository<SummaryCounters> repositorySummaryCount)
        {
            
            this.repositoryApplicationCount = repositoryApplicationCount;
            this.repositorAssignmentGroupCount = repositorAssignmentGroupCount;
            this.repositoryPrimaryBusinessLineCount = repositoryPrimaryBusinessLineCount;
            this.repositoryPersonCount = repositoryPersonCount;
            this.repositorySummaryCount = repositorySummaryCount;
        }

        public IQueryable<ApplicationCount> getApplicationCount()
        {
            var applicationCounts = repositoryApplicationCount.Query();
            //var results = from applicationCount in applicationCounts
            //              group applicationCount by new { applicationCount.ApplicationName} into applicationCountGroup
            //              select (new ApplicationCount { ApplicationName = applicationCountGroup.Key.ApplicationName, Counter = applicationCountGroup.Sum(x => x.Counter) });
            return applicationCounts;
        }

        public IQueryable<AssignmentGroupCount> getAssignmentGroupCount()
        {
            var applicationGroupCounts = repositorAssignmentGroupCount.Query();
            //var results = from applicationGroupCount in applicationGroupCounts
            //              group applicationGroupCount by new { applicationGroupCount.AssignmentGroup } into applicationGroupCountGroup
            //              select (new AssignmentGroupCount { AssignmentGroup = applicationGroupCountGroup.Key.AssignmentGroup, Counter = applicationGroupCountGroup.Sum(x => x.Counter) });

            return applicationGroupCounts;
        }

        public IQueryable<BusinessLineCount> getBusinessLineCount()
        {
            var businessLineCounts = repositoryPrimaryBusinessLineCount.Query().Distinct();
            //var results = from businessLineCount in businessLineCounts
            //              group businessLineCount by new { businessLineCount.BusinessLine } into businessLineCountGroup
            //              select (new BusinessLineCount { BusinessLine = businessLineCountGroup.Key.BusinessLine, Counter = businessLineCountGroup.Sum(x => x.Counter) });

            return businessLineCounts;
        }

        public IQueryable<PersonCount> getPersonCount()
        {
            var personCounts = repositoryPersonCount.Query();
            //var results = from personCount in personCounts
            //              group personCount by new { personCount.personName } into personCountGroup
            //              select (new PersonCount { personName = personCountGroup.Key.personName, Counter = personCountGroup.Sum(x => x.Counter)});

            return personCounts;
        }

        public IQueryable<SummaryCounters> getSummaryCounts()
        {
            var summaryCounts = repositorySummaryCount.Query();
            //var results = from summaryCount in summaryCounts

            //              select (new SummaryCounters { IncidentCounter =  summaryCounts.Sum(x => x.IncidentCounter),
            //                  RequestCounter = summaryCounts.Sum(x => x.RequestCounter), SOICounter = summaryCounts.Sum(x => x.SOICounter),
            //                 NetcoolCounter = summaryCounts.Sum(x => x.NetcoolCounter), TotalIncidents = summaryCounts.Sum(x => x.TotalIncidents)
            //              });

            return summaryCounts;
        }

    }
}
