using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.EmailEntity
{
    public class EmailCc:EmailReceipient
    {
        public EmailCc()
        {
        }
        public EmailCc(string name, string emailAddress)
            : base(name, emailAddress)
        {
        }
    }
}
