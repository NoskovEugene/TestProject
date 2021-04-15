using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.DAL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private ServerDbContext Context { get; set; }

        private DbSet<T> Set { get; set; }
		 
        public RepositoryBase(ServerDbContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        }

        public void Insert(T entity)
        {
            Set.Add(entity);
            Flush();
        }

        public async Task  InsertAsync(T entity)
        {
            await Set.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            Set.Remove(entity);
            Flush();
        }

        public T Find(int id)
        {
            return Set.Find(id);
        }

        public void Update(T entity)
        {
            Set.Update(entity);
            Flush();
        }

        public void Remove(int id)
        {
            var entity = Find(id);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        public IQueryable<T> Query(Expression<Func<T ,bool>> filter)
        {
            return Set.Where(filter);
        }
		
        public int Flush()
        {
            return Context.SaveChanges();
        }

        public Task<int> FlushAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}