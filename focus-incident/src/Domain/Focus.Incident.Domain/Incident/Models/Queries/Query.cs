using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Incident.Domain.Incident.Models.Queries
{
    public class Query
    {
        public State State { get; set; }
    }


    public class State
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public IEnumerable<SortDescriptor> Sort { get; set; }
    }

    public class SortDescriptor
    {
        public string Field { get; set; }
        public string Dir { get; set; }
    }
}
