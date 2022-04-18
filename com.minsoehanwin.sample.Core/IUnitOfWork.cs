using com.minsoehanwin.sample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core
{
    public interface IUnitOfWork:IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
