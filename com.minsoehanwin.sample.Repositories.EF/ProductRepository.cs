using System;
using System.Collections.Generic;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core.Repositories;
using com.minsoehanwin.sample.Core;
using System.Linq;
using log4net;

namespace com.minsoehanwin.sample.Repositories.EF
{
   
    

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            
        }
    }
}