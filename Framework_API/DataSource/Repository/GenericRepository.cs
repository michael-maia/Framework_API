using Framework_API.Data;
using Framework_API.DataSource.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.DataSource.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DBContext _context;

        public GenericRepository(DBContext context)
        {
            _context = context;
        }

        public async Task Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task Remove(int id)
        {
            try
            {
                var entity = await FetchById(id);
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Remove(string id)
        {
            try
            {
                var entity = await FetchById(id);
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FetchById(int id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FetchById(string id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> FetchAll()
        {
            try
            {
                return _context.Set<TEntity>();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}