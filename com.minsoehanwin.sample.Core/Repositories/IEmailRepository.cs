using com.minsoehanwin.sample.Core.EmailEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Repositories
{
    public interface IEmailRepository : IRepositoryBase<Email>
    {
        void AddOrUpdateCommit(Email email);
    }
}
