using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;

namespace Mvc4AppTestWebAPI.Controllers
{
    public class EmailController : BaseController
    {
        private IEmailService _emailService;
        public EmailController(IUnitOfWork unitOfWork, IEmailService emailService)
            : base(unitOfWork)
        {
            _emailService = emailService;
        }

        public string SendEmails()
        {
            string msg = string.Empty;
            try
            {
                _logger.Info("Start sending emails");
                _unitOfWork.BeginTransaction();
                _emailService.SendEmails();
                _unitOfWork.Commit();
                msg += "Transaction committed.Sending emails successful.For any error please see dbo.Email table.";
                _logger.Info(msg);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                msg += "Transaction rolledback.Sending emails failed.Please see dbo.Email table for detail.";
                _logger.Error(new List<Object>(){
                    msg
                }, ex);
            }
            finally
            {
                _logger.Info("Start sending emails.Done.");
            }
            return msg;
        }
    }
}
