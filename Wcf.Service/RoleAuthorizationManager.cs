using com.minsoehanwin.sample.Services.AspNetIdentity;
using log4net;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// <summary>
    /// http://www.allenconway.net/2012/07/using-basic-authentication-in-rest.html
    /// http://www.c-sharpcorner.com/UploadFile/vendettamit/create-secure-wcf-rest-api-with-custom-basic-authentication/
    /// </summary>
    public class RoleAuthorizationManager : ServiceAuthorizationManager
    {
        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            // Find out the roleNames from the user database, 
            // for example, var roleNames = userManager.GetRoles(user.Id).ToArray();
            //var roleNames = new string[] { "Customer" };
            try
            {
                var identityManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                if (HttpContext.Current.Items["userId"] == null)
                {
                    //Actually this whole if block is unnecessary because
                    //IdentityValidator already handled http basic Authentication
                    //if HttpContext.Current.Items["userId"] == null , it will throw exception in IdentityValidator and 
                    //willl not reach here.
                    //This code block is redundent.
                    try
                    {
                        string userId = string.Empty;
                        //http://www.c-sharpcorner.com/UploadFile/vendettamit/create-secure-wcf-rest-api-with-custom-basic-authentication/
                        var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                        if ((authHeader != null) && (authHeader != string.Empty))
                        {
                            var svcCredentials = System.Text.ASCIIEncoding.ASCII
                                .GetString(Convert.FromBase64String(authHeader.Substring(6)))
                                .Split(':');
                            //var svcCredentials = authHeader.Substring(6).Split(':');
                            var userName = svcCredentials[0];
                            var password = svcCredentials[1];
                            Task<ApplicationUser> taskUser = identityManager.FindByNameAsync(userName);
                            Task<bool> loginSuccess = identityManager.CheckPasswordAsync(taskUser.Result, password);
                            if (loginSuccess.Result)
                            {
                                userId = taskUser.Result.Id;
                                HttpContext.Current.Items["RestfuluserId"] = userId;
                            }
                        }
                        else
                        {
                            throw new Exception("Error occour when getting username and password from basic authentication \"Authorization\" http header.");
                        }
                        Task<IList<string>> roleNames = identityManager.GetRolesAsync(userId);
                        //http://www.c-sharpcorner.com/UploadFile/vendettamit/create-secure-wcf-rest-api-with-custom-basic-authentication/
                        operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, roleNames.Result.ToArray());
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Basic authentication failed.",ex);
                        //Throw an exception with the associated HTTP status code equivalent to HTTP status 401  
                        throw new WebFaultException(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    //retrieved role of login userId and put it in AuthorizationContext to be used later in EmployeeService role base authorization
                    var userId = HttpContext.Current.Items["userId"].ToString();
                    Task<IList<string>> roleNames = identityManager.GetRolesAsync(userId);
                    operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, roleNames.Result.ToArray());
                }

                
            }
            catch (WebFaultException ex)
            {
                _logger.Error(new List<Object>() { 
                    "Failed to get role. Challange Basic authentication header(WWW-Authenticate: Basic realm=\"com.minsoehanwin.sample.Wcf.Service\")."
                }, ex);
                ////http://www.c-sharpcorner.com/UploadFile/vendettamit/create-secure-wcf-rest-api-with-custom-basic-authentication/
                //No authorization header was provided, so challenge the client to provide before proceeding:  
                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"com.minsoehanwin.sample.Wcf.Service\"");
                //Throw an exception with the associated HTTP status code equivalent to HTTP status 401  
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.Error(new List<Object>() { 
                    "Failed to get role."
                }, ex);
                throw new FaultException(ex.Message);
            }
            return true;
        }

    }

}
