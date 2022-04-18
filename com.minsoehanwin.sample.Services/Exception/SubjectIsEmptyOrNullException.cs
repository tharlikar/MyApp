using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    class SubjectIsEmptyOrNullException : BaseException
    {
        public SubjectIsEmptyOrNullException(string msg)
            : base(msg)
        {

        }
    }
}
