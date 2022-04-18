using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.minsoehanwin.sample.Core.EmailEntity
{
    public class Email
    {
        
        public Email()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public virtual string Id { get; set; }
        public virtual string Reference { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime? CreatedOn { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? ExpectedDeliveryDateTime { get; set; }
        public virtual DateTime? ActualDeliveryDateTime { get; set; }
        public virtual string DeliveredBy { get; set; }
        public virtual string From { get; set; }
        public virtual string FromName { get; set; }
        public virtual IList<EmailTo> EmailTos { get; set; }
        public virtual IList<EmailCc> EmailCcs { get; set; }
        public virtual IList<EmailBcc> Bccs { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual IList<EmailAttachment> Attachments { get; set; }
    }
}
