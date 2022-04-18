using com.minsoehanwin.sample.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services
{
    public class MockSmtpClient: IEmailClient
    {
        public void SendEmail(string from, string fromName, IList<com.minsoehanwin.sample.Core.EmailEntity.EmailTo> tos, IList<com.minsoehanwin.sample.Core.EmailEntity.EmailCc> ccs, IList<com.minsoehanwin.sample.Core.EmailEntity.EmailBcc> bccs, string subject, string body, IList<com.minsoehanwin.sample.Core.EmailEntity.EmailAttachment> attachments)
        {
            
        }
    }
}
