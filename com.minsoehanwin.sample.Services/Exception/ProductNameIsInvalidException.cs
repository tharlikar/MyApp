using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    class ProductNameIsInvalidException : BaseException
    {
        public ProductNameIsInvalidException(string msg)
            : base(msg)
        {
        }
    }
}
