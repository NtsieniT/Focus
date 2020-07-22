using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Domain.Incident.Interfaces;
using Focus.Incident.Domain.Incident.Models;
using Focus.Incident.Domain.Incident.Models.Queries;
using Focus.Incident.Domain.Report.Models;
using Linq.Extras;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel = Focus.Incident.Domain.Incident.Models;

namespace Focus.Incident.Domain.Incident.Service
{
    public class IncidentImportService
    {
        private readonly IRepository<DomainModel.Incident> repositoryIncident;
        //private readonly IIncidentSNRepository repositoryIncidentSN;
        private readonly IIncidentSFRepository repositoryIncidentSF;
        private ILogger<IncidentImportService> loggingService;
        private readonly IRepository<PrimaryBusinessLine.Models.PrimaryBusinessLine> repositoryPrimaryBusinessLine;
        private readonly IRepository<Person.Models.Person> repositoryPerson;
        private readonly IRepository<Application.Models.Application> repositoryApplication;
        private readonly IRepository<AssignmentGroup.Models.AssignmentGroup> repositoryAssignmentGroup;

        private readonly IRepository<ApplicationCount> repositoryApplicationCount;
        private readonly IRepository<AssignmentGroupCount> repositorAssignmentGroupCount;
        private readonly IRepository<BusinessLineCount> repositoryPrimaryBusinessLineCount;
        private readonly IRepository<PersonCount> repositoryPersonCount;
        private readonly IRepository<SummaryCounters> repositorySummaryCounters;

       
        //CONSTRUCTOR
        public IncidentImportService(IRepository<DomainModel.Incident> repositoryIncident/*, IIncidentSNRepository repositoryIncidentSN*/, IIncidentSFRepository repositoryIncidentSF, ILogger<IncidentImportService> loggingService, IRepository<PrimaryBusinessLine.Models.PrimaryBusinessLine> repositoryPrimaryBusinessLine, IRepository<Person.Models.Person> repositoryPerson, IRepository<Application.Models.Application> repositoryApplication, IRepository<AssignmentGroup.Models.AssignmentGroup> repositoryAssignmentGroup, IRepository<ApplicationCount> repositoryApplicationCount, IRepository<AssignmentGroupCount> repositorAssignmentGroupCount, IRepository<BusinessLineCount> repositoryPrimaryBusinessLineCount, IRepository<PersonCount> repositoryPersonCount, IRepository<SummaryCounters> repositorySummaryCounters)
        {
            this.repositoryIncident = repositoryIncident;
            //this.repositoryIncidentSN = repositoryIncidentSN;
            this.repositoryIncidentSF = repositoryIncidentSF;
            this.loggingService = loggingService;
            this.repositoryPrimaryBusinessLine = repositoryPrimaryBusinessLine;
            this.repositoryPerson = repositoryPerson;
            this.repositoryApplication = repositoryApplication;
            this.repositoryAssignmentGroup = repositoryAssignmentGroup;
            this.repositoryApplicationCount = repositoryApplicationCount;
            this.repositorAssignmentGroupCount = repositorAssignmentGroupCount;
            this.repositoryPrimaryBusinessLineCount = repositoryPrimaryBusinessLineCount;
            this.repositoryPersonCount = repositoryPersonCount;
            this.repositorySummaryCounters = repositorySummaryCounters;
        }

        public async Task Sync()
        {
            // get incidents from SN & SF 
            //var incidentsSNResults = await repositoryIncidentSN.ReadAsync(new IncidentQuery());

            var incidentsSFResults = await repositoryIncidentSF.ReadAsync(new IncidentQuery());

            //summarizeIncidents(incidents);
            EnsureLookUps(incidentsSFResults.Results);
            //EnsureLookUps(incidentsSNResults.Results); //Old Service Now database which is now Obsolete and no longer used

        }


        private void EnsureLookUps(IEnumerable<CMDBIncidentResult> incidents)
        {
            
            //Ensure DB Clean
            ensureDbDelete(incidents);

            //Ensure Incidents
            ensurePrimaryBusinessLine(incidents);
            ensureApplication(incidents);
            ensureApplicationGroup(incidents);
            ensurePerson(incidents);
            ensureIncidents(incidents);

            //COUNTS
            ensureBusinessLineCount(incidents);
            ensurePersonCount(incidents);
            ensureAassignmentGroupCount(incidents);
            ensureApplicationCount(incidents);

            //Summary
            summarizeIncidents(incidents);
        }

        private void ensureIncidents(IEnumerable<CMDBIncidentResult> incident)
        {
            var primarybusinesslines = repositoryPrimaryBusinessLine.Query().ToList();
            var persons = repositoryPerson.Query().ToList();
            var assignmentGroup = repositoryAssignmentGroup.Query().ToList();
            var application = repositoryApplication.Query().ToList();
            var existingIncidents = repositoryIncident.Query().ToList();

            var source = incident
                .Where(x => !string.IsNullOrEmpty(x.Number)).DistinctBy(x => x.Number)
                .Select(x => new DomainModel.Incident
                {
                    Number = x.Number,
                    Assigned_toId = persons.FirstOrDefault(y => y.Name == x.Assigned_to).Id,
                    AssignmentGroup = assignmentGroup.FirstOrDefault(y => y.Name == x.Assignment_Group),
                    Business_Area = x.Business_Area,
                    Business_Impact = x.Business_Impact,
                    PrimaryBusinessLineId = primarybusinesslines.FirstOrDefault(y => y.Name == x.Business_Line).Id,
                    Caller_ID = x.Caller_ID,
                    Caused_By = x.Caused_By,
                    Classification = x.Classification,
                    Closed = x.Closed,
                    ApplicationId = application.FirstOrDefault(y => y.Name == x.CMDB_CI).Id,
                    CMDB_CI_Company = x.CMDB_CI_Company,
                    Company = x.Company,
                    Country_Impacted = x.Country_Impacted,
                    Created = x.Created,
                    Description = x.Description,
                    Last_Updated = x.Last_Updated,
                    Major_Incident = x.Major_Incident,
                    Priority = x.Priority,
                    Problem_ID = x.Problem_ID,
                    Resolution_Description = x.Resolution_Description,
                    Resolved = x.Resolved,
                    Severity = x.Severity,
                    ShortDescription = x.ShortDescription,
                    State = x.State,
                    Vendor = x.Vendor,

                }).ToList();

            var newIncidents = source.ExceptBy(existingIncidents, x => x.Number, StringComparer.OrdinalIgnoreCase);
            foreach (var item in newIncidents)
            {
                repositoryIncident.Add(item);
            }
            if (repositoryIncident.Query().ToList() != null)
            {
                repositoryIncident.SaveChanges();
            }

        }


        private void summarizeIncidents(IEnumerable<CMDBIncidentResult> model)
        {

            var listApplicationCount = new Dictionary<string, ApplicationCount>();

            var summaryCounters = new SummaryCounters();
            summaryCounters.TotalIncidents = model.Count();

            foreach (var incident in model)
            {
                if (incident.Classification.ToUpper().Equals("REQUEST")) { summaryCounters.RequestCounter++; }
                if (incident.Classification.ToUpper().Equals("INCIDENT")) { summaryCounters.IncidentCounter++; }
                if (incident.Caller_ID.ToUpper().Contains("SOI")) { summaryCounters.SOICounter++; }
                if (incident.Caller_ID.ToUpper().Contains("NETCOOL")) { summaryCounters.NetcoolCounter++; }
                repositorySummaryCounters.Add(summaryCounters);

            }
            repositorySummaryCounters.SaveChanges();
        }


        public void ensurePrimaryBusinessLine(IEnumerable<CMDBIncidentResult> cmdbIncidentresults)
        {
            var target = repositoryPrimaryBusinessLine.Query().ToList(); //Target
            var source = cmdbIncidentresults.Select(x => x.Business_Line)
                .Where(x => !string.IsNullOrEmpty(x)).Distinct()
                .Select(x => new PrimaryBusinessLine.Models.PrimaryBusinessLine { Name = x }).ToList(); //Source

            ////Delete Business Line
            //var deletedItems = target.ExceptBy(source, x => x.Name, StringComparer.OrdinalIgnoreCase);
            //foreach (var item in deletedItems)
            //{
            //    repositoryPrimaryBusinessLine.Remove(item);
            //}
            //repositoryPrimaryBusinessLine.SaveChanges();


            //Create new business Line
            var newPrimaryBusinessLines = source.ExceptBy(target, x => x.Name, StringComparer.OrdinalIgnoreCase);
            foreach (var primaryBusinessLineItem in newPrimaryBusinessLines)
            {
                repositoryPrimaryBusinessLine.Add(primaryBusinessLineItem);
            }

            //Update Business Line
            //var updatedItems = target.IntersectBy(source, x => x.Name, StringComparer.OrdinalIgnoreCase);
            //foreach (var item in updatedItems)
            //{
            //    repositoryPrimaryBusinessLine.Update(item);
            //}


            repositoryPrimaryBusinessLine.SaveChanges();
        }
        //Application
        public void ensureApplication(IEnumerable<CMDBIncidentResult> cmdbIncidentresults)
        {
            var target = repositoryApplication.Query().ToList(); //Target
            var source = cmdbIncidentresults.Select(x => x.CMDB_CI)
                .Where(x => !string.IsNullOrEmpty(x)).Distinct()
                .Select(x => new Application.Models.Application { Name = x }).ToList(); //Source

            //Create new Application
            var newApplication = source.ExceptBy(target, x => x.Name, StringComparer.OrdinalIgnoreCase);
            foreach (var application in newApplication)
            {
                repositoryApplication.Add(application);
            }

            //Update Application
            //var updatedItems = target.IntersectBy(source, x => x.Name, StringComparer.OrdinalIgnoreCase);
            //foreach (var item in updatedItems)
            //{
            //    repositoryApplication.Update(item);
            //}

            ////Delete Application
            //var deletedItems = target.ExceptBy(source, x => x.Name, StringComparer.OrdinalIgnoreCase);
            //foreach (var item in deletedItems)
            //{
            //    repositoryApplication.Remove(item);
            //}
            repositoryApplication.SaveChanges();

        }

        //ApplicationGroup
        public void ensureApplicationGroup(IEnumerable<CMDBIncidentResult> cmdbIncidentresults)
        {
            var target = repositoryAssignmentGroup.Query().ToList(); //Target
            var source = cmdbIncidentresults.Select(x => x.Assignment_Group)
                .Where(x => !string.IsNullOrEmpty(x)).Distinct()
                .Select(x => new AssignmentGroup.Models.AssignmentGroup { Name = x }).ToList(); //Source

            //Create new business Line
            var newApplicationGroup = source.ExceptBy(target, x => x.Name, StringComparer.OrdinalIgnoreCase);
            foreach (var applicationGroupItem in newApplicationGroup)
            {
                repositoryAssignmentGroup.Add(applicationGroupItem);
            }

            //Update Business Line
            //var updatedItems = target.IntersectBy(source, x => x.Name, StringComparer.OrdinalIgnoreCase);
            //foreach (var item in updatedItems)
            //{
            //    repositoryAssignmentGroup.Update(item);
            //}
            //Delete Business Line
            //var deletedItems = target.ExceptBy(source, x => x.Name, StringComparer.OrdinalIgnoreCase);
            //foreach (var item in deletedItems)
            //{
            //    repositoryAssignmentGroup.Remove(item);
            //}
            repositoryAssignmentGroup.SaveChanges();

        }

        //Person
        public void ensurePerson(IEnumerable<CMDBIncidentResult> cmdbIncidentresults)
        {
            var target = repositoryPerson.Query().ToList(); //Target
            var source = cmdbIncidentresults.Select(x => x.Assigned_to)
                .Where(x => !string.IsNullOrEmpty(x)).Distinct()
                .Select(x => new Person.Models.Person { Name = x }).ToList(); //Source

            //Create new Person
            var newPerson = source.ExceptBy(target, x => x.Name, StringComparer.OrdinalIgnoreCase);
            foreach (var personItem in newPerson)
            {
                repositoryPerson.Add(personItem);
            }

            //Update Person
            //var updatedItems = target.IntersectBy(source, x => x.Name, StringComparer.OrdinalIgnoreCase);
            //foreach (var item in updatedItems)
            //{
            //    repositoryPerson.Update(item);
            //}

            ////Delete Person
            //var deletedItems = target.ExceptBy(source, x => x.Name, StringComparer.OrdinalIgnoreCase);
            //foreach (var item in deletedItems)
            //{
            //    repositoryPerson.Remove(item);
            //}
            repositoryPerson.SaveChanges();

        }

        #region Counts

        //BusinessLine Count
        public void ensureBusinessLineCount(IEnumerable<CMDBIncidentResult> cmdbIncidentresults)
        {
            ////Delete first and save again //NO LONGER USED
            //var target = repositoryPrimaryBusinessLineCount.Query().ToList();
            //var source = cmdbIncidentresults.Select(x => x.Business_Line)
            //    .Where(x => !string.IsNullOrEmpty(x)).Distinct()
            //    .Select(x => new BusinessLineCount { BusinessLine = x }).ToList(); //Source

            //var deletedItems = target.ExceptBy(source, x => x.BusinessLine, StringComparer.OrdinalIgnoreCase);

            //foreach (var item in deletedItems)
            //{
            //    repositoryPrimaryBusinessLineCount.Remove(item);
            //}
            //repositoryPrimaryBusinessLineCount.SaveChanges();


            var listBusinessLineCount = new Dictionary<string, BusinessLineCount>();

            foreach (var incident in cmdbIncidentresults)
            {
                // Business Line

                BusinessLineCount businessLine = null;


                if (listBusinessLineCount.TryGetValue(incident.Business_Line, out businessLine))
                {
                    businessLine.Counter++;
                }
                else
                {
                    businessLine = new BusinessLineCount();
                    businessLine.BusinessLine = incident.Business_Line.ToString();
                    businessLine.Counter = 1;

                    listBusinessLineCount[incident.Business_Line] = businessLine;
                    repositoryPrimaryBusinessLineCount.Add(businessLine);
                }
            }

            if (repositoryPrimaryBusinessLineCount.Query().ToList() != null)
            {
                repositoryPrimaryBusinessLineCount.SaveChanges();
            }
            //repositoryPrimaryBusinessLineCount.SaveChanges();
        }

        //Person Count
        public void ensurePersonCount(IEnumerable<CMDBIncidentResult> cmdbIncidentresults)
        {

            //var target = repositoryPersonCount.Query().ToList();
            //var source = cmdbIncidentresults.Select(x => x.Assigned_to)
            //    .Where(x => !string.IsNullOrEmpty(x)).Distinct()
            //    .Select(x => new PersonCount { personName = x }).ToList(); //Source

            //var deletedItems = target.ExceptBy(source, x => x.personName, StringComparer.OrdinalIgnoreCase);

            //foreach (var item in deletedItems)
            //{
            //    repositoryPersonCount.Remove(item);
            //}
            //repositoryPersonCount.SaveChanges();


            var listPersonCount = new Dictionary<string, PersonCount>();
            // Person Count 
            PersonCount personCount = null;

            foreach (var incident in cmdbIncidentresults)
            {
                if (listPersonCount.TryGetValue(incident.Assigned_to, out personCount))
                {
                    personCount.Counter++;
                }
                else
                {
                    personCount = new PersonCount
                    {
                        personName = incident.Assigned_to,
                        Counter = 1
                    };

                    listPersonCount[incident.Assigned_to] = personCount;
                    repositoryPersonCount.Add(personCount);
                }
            }

            if (repositoryPersonCount.Query().ToList() != null)
            {
                repositoryPersonCount.SaveChanges();
            }



        }

        //Assignment Group Count
        public void ensureAassignmentGroupCount(IEnumerable<CMDBIncidentResult> cmdbIncidentresults)
        {
            var listAssignmentGroupCount = new Dictionary<string, AssignmentGroupCount>();

            //var target = repositorAssignmentGroupCount.Query().ToList();
            //var source = cmdbIncidentresults.Select(x => x.Assignment_Group)
            //    .Where(x => !string.IsNullOrEmpty(x)).Distinct()
            //    .Select(x => new AssignmentGroupCount { AssignmentGroup = x }).ToList(); //Source

            //var deletedItems = target.ExceptBy(source, x => x.AssignmentGroup, StringComparer.OrdinalIgnoreCase);
            //foreach (var item in deletedItems)
            //{
            //    repositorAssignmentGroupCount.Remove(item);
            //}
            //repositorAssignmentGroupCount.SaveChanges();


            foreach (var incident in cmdbIncidentresults)
            {
                // Assignment Group
                AssignmentGroupCount assignGroup = null;
                if (listAssignmentGroupCount.TryGetValue(incident.Assignment_Group, out assignGroup))
                {
                    assignGroup.Counter++;
                }
                else
                {
                    assignGroup = new AssignmentGroupCount();
                    assignGroup.AssignmentGroup = incident.Assignment_Group;
                    assignGroup.Counter = 1;

                    listAssignmentGroupCount[incident.Assignment_Group] = assignGroup;
                    repositorAssignmentGroupCount.Add(assignGroup);
                }
            }

            if (repositoryAssignmentGroup.Query().ToList() != null)
            {
                repositoryAssignmentGroup.SaveChanges();
            }

        }

        //Application Count
        public void ensureApplicationCount(IEnumerable<CMDBIncidentResult> cmdbIncidentresults)
        {
            //var target = repositoryApplicationCount.Query().ToList();
            //var source = cmdbIncidentresults.Select(x => x.CMDB_CI)
            //    .Where(x => !string.IsNullOrEmpty(x)).Distinct()
            //    .Select(x => new ApplicationCount { ApplicationName = x }).ToList(); //Source

            //var deletedItems = target.ExceptBy(source, x => x.ApplicationName, StringComparer.OrdinalIgnoreCase);

            //foreach (var item in deletedItems)
            //{
            //    repositoryApplicationCount.Remove(item);
            //}
            //repositoryApplicationCount.SaveChanges();


            var listApplicationCount = new Dictionary<string, ApplicationCount>();

            foreach (var incident in cmdbIncidentresults)
            {

                // Application
                ApplicationCount application = null;
                if (listApplicationCount.TryGetValue(incident.CMDB_CI, out application))
                {
                    application.Counter++;
                }
                else
                {
                    application = new ApplicationCount();

                    application.ApplicationName = incident.CMDB_CI;
                    application.Counter = 1;

                    listApplicationCount[incident.CMDB_CI] = application;
                    repositoryApplicationCount.Add(application);
                }

            }

            if (repositoryApplicationCount.Query().ToList() != null)
            {
                repositoryApplicationCount.SaveChanges();
            }


        }



        #endregion Counts


        public void ensureDbDelete(IEnumerable<CMDBIncidentResult> cmdbIncidentresults)
        {

            var incidents = repositoryIncident.Query().ToList();
            var primaryBusinessLines = repositoryPrimaryBusinessLine.Query().ToList();
            var assignmentGroup = repositoryAssignmentGroup.Query().ToList();
            var application = repositoryApplication.Query().ToList();
            var person = repositoryPerson.Query().ToList();

            var primaryBusinessLinesCount = repositoryPrimaryBusinessLineCount.Query().ToList();
            var assignmentGroupCount = repositorAssignmentGroupCount.Query().ToList();
            var applicationCount = repositoryApplicationCount.Query().ToList();
            var personCount = repositoryPersonCount.Query().ToList();
            var summaryCount = repositorySummaryCounters.Query().ToList();

            //Clear DB to get fresh new records on next import
            //if (primaryBusinessLines.ToList() != null && assignmentGroup.ToList() != null && application.ToList() != null && person.ToList() != null && incidents.ToList() != null)
            //{
            foreach (var item in primaryBusinessLines)
            {
                repositoryPrimaryBusinessLine.Remove(item);
            }

            repositoryPrimaryBusinessLine.SaveChanges();

            foreach (var item in assignmentGroup.ToList())
            {
                repositoryAssignmentGroup.Remove(item);
            }

            foreach (var item in application.ToList())
            {
                repositoryApplication.Remove(item);
            }

            foreach (var item in person.ToList())
            {
                repositoryPerson.Remove(item);
            }

            foreach (var item in incidents.ToList())
            {
                repositoryIncident.Remove(item);
            }
            //}

            //Clear DB to get fresh new counts on next import
            //if (primaryBusinessLinesCount.ToList() != null && assignmentGroupCount.ToList() != null && applicationCount.ToList() != null && personCount.ToList() != null && summaryCount.ToList() != null)
            //{
            foreach (var item in primaryBusinessLinesCount.ToList())
            {
                repositoryPrimaryBusinessLineCount.Remove(item);
            }

            foreach (var item in assignmentGroupCount.ToList())
            {
                repositorAssignmentGroupCount.Remove(item);
            }

            foreach (var item in applicationCount.ToList())
            {
                repositoryApplicationCount.Remove(item);
            }

            foreach (var item in personCount.ToList())
            {
                repositoryPersonCount.Remove(item);
            }

            foreach (var item in summaryCount.ToList())
            {
                repositorySummaryCounters.Remove(item);
            }
            //}

            repositoryAssignmentGroup.SaveChanges();
            repositoryApplication.SaveChanges();
            repositoryPerson.SaveChanges();
            repositoryIncident.SaveChanges();

            repositoryPrimaryBusinessLineCount.SaveChanges();
            repositorAssignmentGroupCount.SaveChanges();
            repositoryApplicationCount.SaveChanges();
            repositoryPersonCount.SaveChanges();
            repositorySummaryCounters.SaveChanges();


        }


    }
}
