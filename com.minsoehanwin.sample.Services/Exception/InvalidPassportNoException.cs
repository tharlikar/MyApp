using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    public class InvalidPassportNoException : BaseException
    {
        public InvalidPassportNoException(string msg):base(msg)
        {
        }
    }
}
