using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF
{
    public class PassportInfoRepository : GenericRepository<PassportInfo>, IPassportInfoRepository
    {
        public PassportInfoRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
