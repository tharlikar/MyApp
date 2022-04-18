using com.minsoehanwin.sample.Core.EmailEntity;
using com.minsoehanwin.sample.Core.Repositories;
using com.minsoehanwin.sample.Core.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace com.minsoehanwin.sample.Services
{
    public class EmailService : BaseService, IEmailService, IIdentityMessageService
    {
        private IEmailRepository _emailRepository;
        private EmailValidator _emailValidator;
        private IEmailClient _emailClient;

        public EmailService(IEmailRepository emailRepository
            ,IEmailClient emailClient
            ,EmailValidator emailValidator)
        {
            _emailRepository = emailRepository;
            _emailValidator = emailValidator;
            _emailClient = emailClient;
        }

        public void SendEmails()
        {
            _logger.Info("Start sending emails.");
            var emailsToDeliver = GetPendingEmailsToBeSent();
            var now = DateTime.UtcNow;
            foreach (var email in emailsToDeliver)
            {
                try
                {
                    _emailClient.SendEmail(email.From, email.FromName, email.EmailTos, email.EmailCcs, email.Bccs, email.Subject, email.Body, email.Attachments);
                    email.ActualDeliveryDateTime = now;
                    email.DeliveredBy = "EmailService";
                    email.Comment += "SuccessfullySent;";
                }
                catch (System.Exception ex)
                {
                    email.Comment += string.Format("SentFailed|{0};",ex.Message);
                    _logger.Error(new List<Object>() { 
                        "Email Exception occur. Email info are:"
                        , email
                        ,"Continue sending another email."
                    }, ex);
                }
                Save(email);
                _logger.Info("Email status(either success or failed) is updated.");
            }
            _logger.Info("Sending emails.Done.");
        }

        private IEnumerable<Email> GetPendingEmailsToBeSent()
        {
            _logger.Info("Getting emails list to be sent.");
            var emailsToDeliver = _emailRepository.Get(e => e.ActualDeliveryDateTime == null && (e.ExpectedDeliveryDateTime<=DateTime.UtcNow || e.ExpectedDeliveryDateTime==null));
            _logger.Info(string.Format("{0} emails pending for sending out.", emailsToDeliver.Count()));
            return emailsToDeliver;
        }

        public void AddEmailToBeSendLater(string from, string fromName, IList<EmailTo> tos, IList<EmailCc> ccs
            , IList<EmailBcc> bccs, string subject, string body, IList<EmailAttachment> attachments
            , string reference="", string createdBy="",string comment="", DateTime? expectedDeliveryDateTime = null)
        {
            var email = ComposeEmail(from, ref fromName, tos, ccs, bccs, subject, body, attachments, reference, createdBy, comment, expectedDeliveryDateTime);
            _emailRepository.AddOrUpdate(email);
        }

        private Email ComposeEmail(string from, ref string fromName, IList<EmailTo> tos, IList<EmailCc> ccs, IList<EmailBcc> bccs, string subject, string body, IList<EmailAttachment> attachments, string reference, string createdBy, string comment, DateTime? expectedDeliveryDateTime)
        {
            _emailValidator.ValidateFrom(from);
            fromName = string.IsNullOrEmpty(fromName) ? string.Empty : fromName;
            _emailValidator.ValidateTos(tos);
            _emailValidator.ValidateCcs(ccs);
            _emailValidator.ValidateBccs(bccs);
            _emailValidator.ValidateSubject(subject);
            _emailValidator.ValidateBody(body);
            _emailValidator.ValidateEmailAttachment(attachments);
            var email = new Email();
            email.From = from;
            email.FromName = fromName;
            foreach (var to in tos)
            {
                to.EmailId = email.Id;
                to.Email = email;
            }
            email.EmailTos = tos;
            foreach (var cc in ccs)
            {
                cc.EmailId = email.Id;
                cc.Email = email;
            }
            email.EmailCcs = ccs;
            foreach (var bcc in bccs)
            {
                bcc.EmailId = email.Id;
                bcc.Email = email;
            }
            email.Bccs = bccs;
            email.Subject = subject;
            email.Body = body;
            foreach (var attachment in attachments)
            {
                attachment.EmailId = email.Id;
                attachment.Email = email;
            }
            email.Attachments = attachments;
            email.ExpectedDeliveryDateTime = expectedDeliveryDateTime;
            email.CreatedBy = createdBy;
            email.Comment = comment;
            email.Reference = reference;
            return email;
        }

        public void Save(Email obj)
        {
            _emailRepository.AddOrUpdate(obj);
        }

        public IList<Email> GetList()
        {
            return new List<Email>(_emailRepository.Get());
        }

        public void Delete(Email obj)
        {
            _emailRepository.Delete(obj);
        }

        public void DeleteById(int id)
        {
            var obj = _emailRepository.GetByID(id);
            _emailRepository.Delete(obj);
        }

        public Email GetById(int id)
        {
            return _emailRepository.GetByID(id);
        }

        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            this.AddEmailToBeSendLaterCommit("minsoehanwin@gmail.com"
                , "Min Soe Han Win"
                , new List<EmailTo>() { new EmailTo(message.Destination, message.Destination) }
                , new List<EmailCc>() { }
                , new List<EmailBcc>() { }
                , message.Subject
                , message.Body
                , new List<EmailAttachment>() { }
                ,string.Empty
                ,string.Empty
                ,string.Empty
                ,null);
            this.SendEmails();
            return Task.FromResult(0);
        }


        public void AddEmailToBeSendLaterCommit(string from, string fromName, IList<EmailTo> tos, IList<EmailCc> ccs, IList<EmailBcc> bccs, string subject, string body, IList<EmailAttachment> attachments, string reference = "", string createdBy = "", string comment = "", DateTime? expectedDeliveryDateTime = null)
        {
            var email = ComposeEmail(from, ref fromName, tos, ccs, bccs, subject, body, attachments, reference, createdBy, comment, expectedDeliveryDateTime);
            _emailRepository.AddOrUpdateCommit(email);
        }
    }
}
