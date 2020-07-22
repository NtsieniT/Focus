using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Incident.Domain.Incident.Models.Queries
{
    public class QueryResult<T>
    {
        public int? Total { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
