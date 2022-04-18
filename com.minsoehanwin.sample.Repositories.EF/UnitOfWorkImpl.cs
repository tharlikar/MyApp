using System;
using System.Collections.Generic;
using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Repositories.EF;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Data.Entity;
using com.minsoehanwin.sample.Core.Repositories;
using log4net;

namespace com.minsoehanwin.sample.Repositories.EF
{
    //see link for EF6 onword DbContext transaction and connection
    //https://msdn.microsoft.com/en-us/data/dn456843.aspx
    public class UnitOfWorkImpl : IUnitOfWork
    {
        public Guid Id = Guid.NewGuid();
        private MyDataContext _context;
        private DbContextTransaction _transaction;

        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public UnitOfWorkImpl(MyDataContext context)
        {
            _context = context;
        }
        public MyDataContext GetCurrentSession()
        {
            return _context;
        }

        #region Implementation of ITransactionProvider

        public void BeginTransaction()
        {
            //see http://www.entityframeworktutorial.net/entityframework6/database-command-logging.aspx
            GetCurrentSession().Database.Log = _logger.Debug;

            _transaction = GetCurrentSession().Database.BeginTransaction();
        }

        public void Commit()
        {
            GetCurrentSession().SaveChanges();
            _transaction.Commit();
            DisposeTransaction(_transaction);
        }

        private void DisposeTransaction(DbContextTransaction tran)
        {
            if (tran != null)
                tran.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            DisposeTransaction(_transaction);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            var conOpen = _transaction != null;
            DisposeTransaction(_transaction);
            DisposeSession(_context);
        }

        private void DisposeSession(DbContext _context)
        {
            if (_context != null)
                _context.Dispose();
        }

        #endregion
    }
}