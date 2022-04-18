using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    class ProductIsNullException : BaseException
    {
        public ProductIsNullException(string msg)
            : base(msg)
        {
        }
    }
}
