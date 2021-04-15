using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Models;

namespace Server.DAL.Repositories
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        /// <summary>
        /// Insert new entity
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);
		
        /// <summary>
        /// Insert entity async 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(T entity);
		
        /// <summary>
        /// Remove entity 
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);
		
        /// <summary>
        /// Find entity and return entity of default
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(int id);
		
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
		
        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);
		
        /// <summary>
        /// Query context
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T ,bool>> filter);
		
        /// <summary>
        /// Save changes to database
        /// </summary>
        /// <returns></returns>
        int Flush();
		
        /// <summary>
        /// Save changes to database async
        /// </summary>
        /// <returns></returns>
        Task<int> FlushAsync();
    }
}