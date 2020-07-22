using Focus.Incident.Domain.Incident.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Incident.Domain.Report.Models
{
    public class ApplicationCount : EntityBase
    {
        
        public string ApplicationName { get; set; }
        public int Counter { get; set; }
    }
}
