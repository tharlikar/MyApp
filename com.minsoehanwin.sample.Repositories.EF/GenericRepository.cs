using log4net;
using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF
{
    //see link for Generic repository concept
    //http://www.tugberkugurlu.com/archive/generic-repository-pattern-entity-framework-asp-net-mvc-and-unit-testing-triangle
    //http://www.itworld.com/article/2700950/development/a-generic-repository-for--net-entity-framework-6-with-async-operations.html
    public class GenericRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class 
        ,new()//https://msdn.microsoft.com/en-us/library/sd2w2ew5.aspx
    {
        private MyDataContext context;
        protected DbSet<TEntity> dbSet;
        public UnitOfWorkImpl _unitOfWork;
        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = (UnitOfWorkImpl)unitOfWork;
            this.context =_unitOfWork.GetCurrentSession();
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void AddOrUpdate(TEntity entity)
        {
            PropertyInfo pInfo=entity.GetType().GetProperty("Id");
            pInfo=(pInfo==null)?entity.GetType().GetProperty("EmployeeId"):pInfo;
            object Id = pInfo.GetValue(entity, null);
            bool insert = true;
            if (Id is int)
            {
                int myId = (int)Id;
                insert = myId <= 0;
            }
            else if (Id is string)
            {
                string myId = (string)Id;
                var e = GetByID(myId);
                insert = e == null;
            }
            else if (Id == null)
            {
                insert = true;
            }
            if (!insert)
            {
                Update(entity);
            }
            else
            {
                Insert(entity);
            }
        }
    }
}
