using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Incident.Domain.Incident.Common.Interface
{
    public interface IRepositoryWithTypedId<T, in TId> where T : IEntityWithTypedId<TId>
    {
        IQueryable<T> Query();

        void Add(T entity);

        void SaveChanges();

        System.Threading.Tasks.Task SaveChangesAsync();

        void Remove(T entity);

        IQueryable<T> QueryWithInclude(params Expression<Func<T, object>>[] includes);
    }
}
