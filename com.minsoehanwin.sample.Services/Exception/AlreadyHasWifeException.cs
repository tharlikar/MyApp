using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    public class AlreadyHasWifeException : BaseException
    {
        public AlreadyHasWifeException(string message)
            : base(message)
        {
        }
    }
}
