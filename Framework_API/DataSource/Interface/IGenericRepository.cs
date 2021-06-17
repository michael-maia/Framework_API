using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.DataSource.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> FetchAll();
        Task<TEntity> FetchById(int id);
        Task<TEntity> FetchById(string id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(int id);
        Task Remove(string id);
    }
}
