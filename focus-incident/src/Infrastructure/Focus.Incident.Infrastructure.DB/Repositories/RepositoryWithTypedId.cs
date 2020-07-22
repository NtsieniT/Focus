using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Infrastructure.DB.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Focus.Incident.Infrastructure.DB.Repositories
{
    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class, IEntityWithTypedId<TId>
    {
        public RepositoryWithTypedId(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }
        protected DbContext Context { get; }
        protected DbSet<T> DbSet { get; }
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
        public System.Threading.Tasks.Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
        public IQueryable<T> Query()
        {
            return DbSet;
        }
        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
        public IQueryable<T> QueryWithInclude(params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(Query(), (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
