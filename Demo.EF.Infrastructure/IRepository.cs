using System;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.EF.Infrastructure
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Gets entity by keys.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        TEntity GetByKey(params object[] keyValues);

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery();
        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Attach(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes one or many entities matching the specified criteria
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        void Delete(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Updates changes of the existing entity. 
        /// The caller must later call SaveChanges() on the repository explicitly to save the entity to database
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Counts the specified entities.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Counts entities with the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        IUnitOfWork UnitOfWork { get; set; }
    }
}
