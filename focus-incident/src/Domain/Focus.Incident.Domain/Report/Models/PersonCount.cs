using Focus.Incident.Domain.Incident.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Incident.Domain.Report.Models
{
    public class PersonCount : EntityBase
    {
        
        public string personName { get; set; }
        public int Counter { get; set; }
    }
}
