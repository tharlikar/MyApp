using log4net;
using com.minsoehanwin.sample.Core.EmailEntity;
using com.minsoehanwin.sample.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace com.minsoehanwin.sample.Services
{
    public class GmailSmtpClient : IEmailClient
    {
        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void SendEmail(string from, string fromName, IList<EmailTo> tos, IList<EmailCc> ccs, IList<EmailBcc> bccs, string subject, string body, IList<EmailAttachment> attachments)
        {
            // Create a message and set up the recipients.
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from, fromName);
            foreach (var to in tos)
            {
                message.To.Add(new MailAddress(to.EmailAddress, to.Name));
            }
            foreach (var cc in ccs)
            {
                message.CC.Add(new MailAddress(cc.EmailAddress, cc.Name));
            }
            foreach (var bcc in bccs)
            {
                message.Bcc.Add(new MailAddress(bcc.EmailAddress, bcc.Name));
            }
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            foreach (var file in attachments)
            {
                Stream fileStream = File.OpenRead(file.PhysicalFilePath);
                // Create  the file attachment for this e-mail message.
                Attachment data = new Attachment(fileStream, file.FileName, MediaTypeNames.Application.Octet);
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file.PhysicalFilePath);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file.PhysicalFilePath);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file.PhysicalFilePath);
                // Add the file attachment to this e-mail message.
                message.Attachments.Add(data);
                // Display the values in the ContentDisposition for the attachment.
                ContentDisposition cd = data.ContentDisposition;
                _logger.Info("Content disposition");
                _logger.Info(cd.ToString());
                _logger.Info(string.Format("File {0}", cd.FileName));
                _logger.Info(string.Format("Size {0}", cd.Size));
                _logger.Info(string.Format("Creation {0}", cd.CreationDate));
                _logger.Info(string.Format("Modification {0}", cd.ModificationDate));
                _logger.Info(string.Format("Read {0}", cd.ReadDate));
                _logger.Info(string.Format("Inline {0}", cd.Inline));
                _logger.Info(string.Format("Parameters: {0}", cd.Parameters.Count));
                foreach (DictionaryEntry d in cd.Parameters)
                {
                    _logger.Info(string.Format("{0} = {1}", d.Key, d.Value));
                }
            }
            //Send the message.
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            // Add credentials if the SMTP server requires them.,
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("minsoehanwin@gmail.com", "#mshw~!@");
            client.Send(message);
        }
    }
}