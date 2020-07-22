using Focus.Incident.Domain.Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Incident.Domain.Report.Interfaces
{
        public interface IReportRepository<T> where T : class
        {
            IQueryable<T> Query();
            IQueryable<T> QueryWithInclude(params Expression<Func<T, object>>[] includes);
        }
    
}
