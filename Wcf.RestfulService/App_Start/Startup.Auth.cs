using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using com.minsoehanwin.sample.Services.AspNetIdentity;
using com.minsoehanwin.sample.Services.AspNetIdentity.Providers;
using Owin;
using System;
using com.minsoehanwin.sample.Repositories.EF;

namespace com.minsoehanwin.sample.Wcf.RestfulService
{
    public partial class Startup
    {
        // For more information on configuring authentication, 
        // please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and role manager 
            // to use a single instance per request
            app.CreatePerOwinContext(MyDataContext.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
        }
    }
}