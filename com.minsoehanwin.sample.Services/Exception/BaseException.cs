using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    public class BaseException:System.Exception
    {
        public BaseException(string message)
            : base(message)
        {

        }
    }
}
