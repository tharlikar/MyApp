using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    class NoEmployeeFoundInStoreException : BaseException
    {
        public NoEmployeeFoundInStoreException(string msg)
            : base(msg)
        {

        }
    }
}
