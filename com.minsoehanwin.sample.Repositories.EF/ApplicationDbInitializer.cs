using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using com.minsoehanwin.sample.Core.AspNetIdentity.Models;

namespace com.minsoehanwin.sample.Repositories.EF
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<MyDataContext>
    {
        protected override void Seed(MyDataContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(MyDataContext db)
        {
            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            //const string name = "admin@example.com";
            //const string password = "Admin@123456";
            //const string roleName = "Admin";

            ////Create Role Admin if it does not exist
            //var role = roleManager.FindByName(roleName);
            //if (role == null)
            //{
            //    role = new IdentityRole(roleName);
            //    var roleresult = roleManager.Create(role);
            //}

            //var user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    user = new ApplicationUser { UserName = name, Email = name };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //}

            //// Add user admin to Role Admin if not already added
            //var rolesForUser = userManager.GetRoles(user.Id);
            //if (!rolesForUser.Contains(role.Name))
            //{
            //    var result = userManager.AddToRole(user.Id, role.Name);
            //}
        }
    }

}
