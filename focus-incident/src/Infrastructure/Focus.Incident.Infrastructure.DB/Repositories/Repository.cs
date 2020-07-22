using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Infrastructure.DB.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Incident.Infrastructure.DB.Repositories
{
    public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
    where T : class, IEntityWithTypedId<long>
    {
        public Repository(ApplicationDbContext context) : base(context)
        {
            //Reseeds database id on demand to id:0
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[Incident]', RESEED, 0)");
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[Application]', RESEED, 0)");
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[ApplicationCount]', RESEED, 0)");
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[AssignmentGroup]', RESEED, 0)");
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[AssignmentGroupCount]', RESEED, 0)");
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[BusinessLineCount]', RESEED, 0)");
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[Person]', RESEED, 0)");
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[PersonCount]', RESEED, 0)");
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[PrimaryBusinessLine]', RESEED, 0)");
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[SummaryCount]', RESEED, 0)");
        }
    }

}
