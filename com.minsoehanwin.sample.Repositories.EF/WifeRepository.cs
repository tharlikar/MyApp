using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF
{
    public class WifeRepository : GenericRepository<Wife>, IWifeRepository
    {
        public WifeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }

}
