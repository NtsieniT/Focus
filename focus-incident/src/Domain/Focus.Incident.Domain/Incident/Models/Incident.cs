using Focus.Incident.Domain.Incident.Common.Model;
using Focus.Incident.Domain.PrimaryBusinessLine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Incident.Domain.Incident.Models
{
    public class Incident : EntityBase 
    {
        #region Class Variables
        public string Number { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Resolved { get; set; }
        public DateTime? Closed { get; set; }
        public DateTime? Last_Updated { get; set; }       
        public string Severity { get; set; }
        public string Priority { get; set; }
        public string ShortDescription { get; set; } 
        public string CMDB_CI_Company { get; set; }
        public string Country_Impacted { get; set; }
        //PBl
        public long? PrimaryBusinessLineId { get; set; }
        public PrimaryBusinessLine.Models.PrimaryBusinessLine PrimaryBusinessLine { get; set; }
        //Application
        public long? ApplicationId { get; set; }
        public Application.Models.Application Application { get; set; }
        //Assignment Group
        public long? AssignmentGroupId { get; set; }
        public AssignmentGroup.Models.AssignmentGroup AssignmentGroup { get; set; }
        //Person
        public long? Assigned_toId { get; set; }
        public Person.Models.Person Assigned_to { get; set; }
        //
        public string Caused_By { get; set; }       
        public string State { get; set; }
        public float? Business_Impact { get; set; }        
        public string Caller_ID { get; set; }
        public string Classification { get; set; }
        public string Description { get; set; }
        public string Resolution_Description { get; set; }
        public string Vendor { get; set; }
        public bool Major_Incident { get; set; }
        public string Problem_ID { get; set; }
        public string Company { get; set; }
        public string Business_Area { get; set; }
        #endregion Class Variables
    }
}
