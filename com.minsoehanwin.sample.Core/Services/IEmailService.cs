using com.minsoehanwin.sample.Core.EmailEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Services
{
    public interface IEmailService : IServiceBase<Email>
    {
        void SendEmails();
        void AddEmailToBeSendLater(string from, string fromName, IList<EmailTo> tos, IList<EmailCc> ccs
            , IList<EmailBcc> bccs, string subject, string body, IList<EmailAttachment> attachments
            , string reference = "", string createdBy = "", string comment = "", DateTime? expectedDeliveryDateTime = null);
        void AddEmailToBeSendLaterCommit(string from, string fromName, IList<EmailTo> tos, IList<EmailCc> ccs
            , IList<EmailBcc> bccs, string subject, string body, IList<EmailAttachment> attachments
            , string reference = "", string createdBy = "", string comment = "", DateTime? expectedDeliveryDateTime = null);
    }
}
