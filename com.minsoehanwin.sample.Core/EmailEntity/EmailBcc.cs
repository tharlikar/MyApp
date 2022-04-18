using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.EmailEntity
{
    public class EmailBcc:EmailReceipient
    {
        public EmailBcc()
        {
        }
        public EmailBcc(string name, string emailAddress)
            : base(name, emailAddress)
        {
        }
    }
}
