using com.minsoehanwin.sample.Services.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    public class EmployeeFirstNameException : BaseException
    {
        public EmployeeFirstNameException(string message)
            : base(message)
        {
        }
    }
}
