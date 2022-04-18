using com.minsoehanwin.sample.Core.EmailEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Services
{
    public interface IEmailClient
    {
        void SendEmail(string from, string fromName, IList<EmailTo> tos, IList<EmailCc> ccs, IList<EmailBcc> bccs, string subject, string body, IList<EmailAttachment> attachments);
    }
}
