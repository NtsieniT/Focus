using Focus.Incident.Domain.Report.Interfaces;
using Focus.Incident.Infrastructure.DB.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Incident.Infrastructure.DB.Repositories
{
    public class ReportRepository<T> : IReportRepository<T>
       where T : Domain.Incident.Common.Model.EntityBase
    {
        public ReportRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }
        protected DbContext Context { get; }
        protected DbSet<T> DbSet { get; }
        public IQueryable<T> Query()
        {
            return DbSet;
        }
        public IQueryable<T> QueryWithInclude(params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(Query(), (current, includeProperty) => current.Include(includeProperty));
        }
     
    }
    
}
