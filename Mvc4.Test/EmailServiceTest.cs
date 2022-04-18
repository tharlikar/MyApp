using com.minsoehanwin.sample.Core.EmailEntity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Test
{
    [TestFixture]
    public class EmailServiceTest : BaseTest
    {
        [Test]
        public void AddMailTest()
        {
            _unitOfWork.BeginTransaction();
            _emailService.AddEmailToBeSendLater("minsoehanwin@gmail.com"
                , "Min Soe Han Win"
                , new List<EmailTo>() { 
                    new EmailTo("Soe Han Win","soehanwin@gmail.com")
                    ,new EmailTo("Tharlikar","tharlikar@gmail.com")
                }
                , new List<EmailCc>() { 
                    new EmailCc("Soe Han Win","soehanwin@gmail.com")
                    ,new EmailCc("Tharlikar","tharlikar@gmail.com")
                }
                , new List<EmailBcc>() { 
                    new EmailBcc("Soe Han Win","soehanwin@gmail.com")
                    ,new EmailBcc("Tharlikar","tharlikar@gmail.com")
                }
                , "Test email service"
                , "<u><b>Test email body</b></u>"
                , new List<EmailAttachment>()
                {
                    new EmailAttachment("JobsLinks.txt",@"C:\Temp\JobsLinks.txt")
                }
                , "ref"
                , "mshw"
                , "comment"
                , DateTime.UtcNow);
            _unitOfWork.Commit();
            var e = _emailService.GetList().Where(x => x.Reference == "ref").FirstOrDefault();
            Assert.That(e.Reference == "ref");
            Assert.That(e.EmailTos.Count == 2);
            Assert.That(e.EmailCcs.Count == 2);
            Assert.That(e.Attachments.Count == 1);
            Assert.That(e.Bccs.Count == 2);
            _unitOfWork.BeginTransaction();
            _emailService.Delete(e);
            _unitOfWork.Commit();
            e = _emailService.GetList().Where(x => x.Reference == "ref").FirstOrDefault();
            Assert.IsNull(e);
        }

        [Test]
        public void ProcessEmailTest()
        {
            _unitOfWork.BeginTransaction();
            _emailService.AddEmailToBeSendLater("minsoehanwin@gmail.com"
                , "Min Soe Han Win"
                , new List<EmailTo>() { 
                    new EmailTo("Soe Han Win","soehanwin@gmail.com")
                    ,new EmailTo("Tharlikar","tharlikar@gmail.com")
                }
                , new List<EmailCc>() { 
                    new EmailCc("Soe Han Win","soehanwin@gmail.com")
                    ,new EmailCc("Tharlikar","tharlikar@gmail.com")
                }
                , new List<EmailBcc>() { 
                    new EmailBcc("Soe Han Win","soehanwin@gmail.com")
                    ,new EmailBcc("Tharlikar","tharlikar@gmail.com")
                }
                , "Test email service"
                , "<u><b>Test email body</b></u>"
                , new List<EmailAttachment>()
                {
                    new EmailAttachment("JobsLinks.txt",@"C:\Temp\JobsLinks.txt")
                }
                , "ref"
                , "mshw"
                , "comment"
                , DateTime.UtcNow);
            _unitOfWork.Commit();
            var e = _emailService.GetList().Where(x => x.Reference == "ref").FirstOrDefault();
            Assert.That(e.Reference == "ref");
            Assert.That(e.EmailTos.Count == 2);
            Assert.That(e.EmailCcs.Count == 2);
            Assert.That(e.Bccs.Count == 2);
            Assert.IsNull(e.ActualDeliveryDateTime);
            _unitOfWork.BeginTransaction();
            _emailService.SendEmails();
            _unitOfWork.Commit();
            e = _emailService.GetList().Where(x => x.Reference == "ref").FirstOrDefault();
            Assert.IsNotNull(e.ActualDeliveryDateTime);
            _unitOfWork.BeginTransaction();
            _emailService.Delete(e);
            _unitOfWork.Commit();
            e = _emailService.GetList().Where(x => x.Reference == "ref").FirstOrDefault();
            Assert.IsNull(e);
        }
    }
}