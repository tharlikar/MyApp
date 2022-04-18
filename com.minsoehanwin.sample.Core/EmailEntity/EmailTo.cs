using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.EmailEntity
{
    public class EmailTo:EmailReceipient
    {
        public EmailTo()
        {
        }
        public EmailTo(string name, string emailAddress)
            : base(name, emailAddress)
        {
        }
    }
}
