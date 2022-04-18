using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    public class NoEmployeeFoundException : BaseException
    {
        public NoEmployeeFoundException(string msg)
            : base(msg)
        {
        }
    }
}
