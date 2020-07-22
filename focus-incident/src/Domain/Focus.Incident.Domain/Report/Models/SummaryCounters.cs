using Focus.Incident.Domain.Incident.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Incident.Domain.Report.Models
{
    public class SummaryCounters : EntityBase
    {
        
        public int RequestCounter { get; set; }
        public int IncidentCounter { get; set; }
        public int SOICounter { get; set; }
        public int NetcoolCounter { get; set; }
        public int TotalIncidents { get; set; } //because there are both incidents and requests this ocunter is for the sum of the two 
    }
}
