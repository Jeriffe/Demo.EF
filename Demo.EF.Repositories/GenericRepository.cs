using Demo.EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.EF.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private bool disposed = false;
        private IUnitOfWork<DbContext> unitOfWork;


        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitOfWork;
            }
            set
            {
                if (!(value is IUnitOfWork<DbContext>))
                {
                    throw new ArgumentException("Expected IUnitOfWork<System.Data.Entity.DbContext>");
                }

                unitOfWork = value as IUnitOfWork<DbContext>;
            }
        }

        private DbContext DbContext
        {
            get
            {
                if (unitOfWork == null)
                {
                    return null;
                }

                return unitOfWork.Context;
            }
        }
        #region IRepository<TEntity>

        public TEntity GetByKey(params object[] keyValues)
        {
            return DbContext.Set<TEntity>().Find(keyValues);
        }

        public IQueryable<TEntity> GetQuery()
        {

            return DbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().SingleOrDefault(criteria);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Where(criteria).FirstOrDefault();
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Attach(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Attach(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> criteria)
        {
            IEnumerable<TEntity> records = GetQuery(criteria);

            foreach (TEntity record in records)
            {
                Delete(record);
            }
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
        }

        public int Count()
        {
            return GetQuery().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Count(criteria);
        }
        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (this.unitOfWork != null)
                    {
                        this.unitOfWork.Dispose();
                        this.unitOfWork = null;
                    }
                }

                disposed = true;
            }
        }

        ~GenericRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}
