using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    public class ProductAlreadExistInStoreException : BaseException
    {
        public ProductAlreadExistInStoreException(string msg)
            : base(msg)
        {
        }
    }
}
