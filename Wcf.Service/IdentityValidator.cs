using com.minsoehanwin.sample.Services.AspNetIdentity;
using log4net;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity.Owin;
using System.ServiceModel.Web;
using System.Net;
using com.minsoehanwin.sample.Core.AspNetIdentity.Models;
namespace com.minsoehanwin.sample.Wcf.Service
{
    public class IdentityValidator : UserNamePasswordValidator
    {
        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override void Validate(string userName, string password)
        {
            try
            {
                bool loginSuccess = false;
                var identityManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    //userName and password are not provided by WCF client
                    //but provided via Basic Authentication/Authorization HTTP header by Restful http request client.
                    var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                    if ((authHeader != null) && (authHeader != string.Empty))
                    {
                        var svcCredentials = System.Text.ASCIIEncoding.ASCII
                            .GetString(Convert.FromBase64String(authHeader.Substring(6)))
                            .Split(':');
                        //var svcCredentials = authHeader.Substring(6).Split(':');
                        userName = svcCredentials[0];
                        password = svcCredentials[1];
                    }
                }
                //userName and password are provided by the WCF client via SSL
                Task<ApplicationUser> user = identityManager.FindByNameAsync(userName);
                Task<bool> loginSuccessFul = identityManager.CheckPasswordAsync(user.Result, password);
                loginSuccess = loginSuccessFul.Result;
                if (!loginSuccess)
                {
                    var msg = String.Format("Unknown Username {0} or incorrect password {1}", userName, password);
                    _logger.Info(msg);
                    throw new Exception(msg);
                }
                //Store ApplicationUser.Id in the global array to be used later by RoleAuthorizationManager
                HttpContext.Current.Items.Add("userId", user.Result.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(new List<Object>() { 
                    "User login failed. UserName:"
                    ,userName
                    ,"Password:"
                    ,password
                }, ex);
                throw new FaultException(ex.Message);
            }
        }
    }
}