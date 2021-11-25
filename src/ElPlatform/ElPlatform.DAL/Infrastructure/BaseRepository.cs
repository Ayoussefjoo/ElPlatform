using ElPlatform.DAL.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ElPlatform.DAL.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T>
 where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        internal DbSet<T> dbSet;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            dbSet = _unitOfWork.Db.Set<T>();
        }

        /// <summary>
        /// Returns the object with the primary key specifies or the default for the type
        /// </summary>
        /// <typeparam name="TU">The type to map the result to</typeparam>
        /// <param name="primaryKey">The primary key</param>
        /// <returns>The result mapped to the specified type</returns>

        public T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            var dbResult = dbSet.FirstOrDefault(where);
            return dbResult;
        }

        public bool Exists(Expression<Func<T, bool>> where)
        {
            return dbSet.SingleOrDefault(where) == null ? false : true;
        }

        public virtual T Insert(T entity)
        {
            dynamic obj = dbSet.Add(entity);
            return obj;
        }

        public virtual T Update(T entity)
        {
            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public void Delete(T entity)
        {
            if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dynamic obj = dbSet.Remove(entity);
        }
        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }
        internal DbContext Database { get { return _unitOfWork.Db; } }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> where, Expression<Func<T, bool>> orderby)
        {
            return dbSet.AsQueryable().Where(where).OrderBy(orderby);
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return dbSet.AsQueryable().Where(where);
        }
    }
}
