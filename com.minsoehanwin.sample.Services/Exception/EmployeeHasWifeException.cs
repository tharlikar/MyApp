﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services.Exception
{
    class EmployeeHasWifeException : BaseException
    {
        public EmployeeHasWifeException(string msg)
            : base(msg)
        {
        }
    }

}
