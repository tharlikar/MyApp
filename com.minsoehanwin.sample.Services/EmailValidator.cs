using com.minsoehanwin.sample.Core.EmailEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services
{
    public class EmailValidator
    {
        public void ValidateSubject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
            {
                throw new com.minsoehanwin.sample.Services.Exception.SubjectIsEmptyOrNullException("Email subject is empty.");
            }
        }

        public void ValidateEmailAttachment(IList<EmailAttachment> attachments)
        {
            if (attachments == null)
            {
                attachments = new List<EmailAttachment>();
            }
            foreach (var at in attachments)
            {
                if (!File.Exists(at.PhysicalFilePath))
                {
                    throw new com.minsoehanwin.sample.Services.Exception.AttachmentFileNotFoundException("Email attach file not found.");
                }
                if (string.IsNullOrEmpty(at.FileName))
                {
                    throw new com.minsoehanwin.sample.Services.Exception.AttachmentFileNotFoundException("Email attachment file name is missing.");
                }
            }
        }

        public void ValidateBody(string body)
        {
            if (string.IsNullOrEmpty(body))
            {
                throw new com.minsoehanwin.sample.Services.Exception.EmailBodyIsNullException("Email body is null.");
            }
        }

        public void ValidateBccs(IList<EmailBcc> bccs)
        {
            if (bccs == null)
            {
                bccs = new List<EmailBcc>();
            }
            foreach (var bcc in bccs)
            {
                if (!new ValidationHelper.RegexUtilities().IsValidEmail(bcc.EmailAddress))
                {
                    throw new com.minsoehanwin.sample.Services.Exception.InvalidEmailAddressException("BCC: email address is not valid.");
                }
                if (string.IsNullOrEmpty(bcc.Name))
                {
                    bcc.Name = bcc.EmailAddress;
                }
            }
        }

        public void ValidateCcs(IList<EmailCc> ccs)
        {
            if (ccs == null)
            {
                ccs = new List<EmailCc>();
            }
            foreach (var cc in ccs)
            {
                if (!new ValidationHelper.RegexUtilities().IsValidEmail(cc.EmailAddress))
                {
                    throw new com.minsoehanwin.sample.Services.Exception.InvalidEmailAddressException("CC: email address is not valid.");
                }
                if (string.IsNullOrEmpty(cc.Name))
                {
                    cc.Name = cc.EmailAddress;
                }
            }
        }

        public void ValidateTos(IList<EmailTo> tos)
        {
            if (tos == null)
            {
                tos = new List<EmailTo>();
            }
            foreach (var to in tos)
            {
                if (!new ValidationHelper.RegexUtilities().IsValidEmail(to.EmailAddress))
                {
                    throw new com.minsoehanwin.sample.Services.Exception.InvalidEmailAddressException("TO: email address is not valid.");
                }
                if (string.IsNullOrEmpty(to.Name))
                {
                    to.Name = to.EmailAddress;
                }
            }
        }

        public void ValidateFrom(string from)
        {
            if (!new ValidationHelper.RegexUtilities().IsValidEmail(from))
            {
                throw new com.minsoehanwin.sample.Services.Exception.InvalidEmailAddressException("From email address is not valid.");
            }
        }
    }
}
