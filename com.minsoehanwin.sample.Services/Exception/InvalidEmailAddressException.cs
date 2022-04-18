using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    class InvalidEmailAddressException : BaseException
    {
        public InvalidEmailAddressException(string msg)
            : base(msg)
        {
        }
    }
}
