using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace com.minsoehanwin.sample.Core.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class 
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        void AddOrUpdate(TEntity entity);
    }
}