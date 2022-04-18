using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    class EmailBodyIsNullException : BaseException
    {
        public EmailBodyIsNullException(string msg)
            : base(msg)
        {

        }
    }
}
