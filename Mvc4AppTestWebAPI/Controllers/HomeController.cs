using com.minsoehanwin.sample.Core.AspNetIdentity.Models;
using com.minsoehanwin.sample.Services.AspNetIdentity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Collections.Generic;
using com.minsoehanwin.sample.Core;
using System;
using com.minsoehanwin.sample.Repositories.EF;
using System.Data.Entity;

namespace Mvc4AppTestWebAPI.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }
        public ActionResult Index()
        {
            //throw new Exception("oop");
            ViewBag.ActiveHome = "active";
            return View();
        }
        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public async Task<string> Seed()
        {
            try
            {
                var context = HttpContext.GetOwinContext().Get<MyDataContext>();
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
                const string name = "admin@example.com";
                const string password = "Admin@123456";
                const string roleName = "Admin";

                var role = await roleManager.FindByNameAsync(roleName);
                //Create Role Admin if it does not exist
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    var roleresult = await roleManager.CreateAsync(role);
                }

                var user = await userManager.FindByNameAsync(name);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = name, Email = name };
                    var result = await userManager.CreateAsync(user, password);
                    var result2 = await userManager.SetLockoutEnabledAsync(user.Id, false);
                }

                // Add user admin to Role Admin if not already added
                var rolesForUser = await userManager.GetRolesAsync(user.Id);
                if (!rolesForUser.Contains(role.Name))
                {
                    var result = await userManager.AddToRoleAsync(user.Id, role.Name);
                }
                return "Successfully created user admin@example.com";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "failed";
        }

        public ActionResult About()
        {
            ViewBag.ActiveAbout = "active";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.ActiveContact = "active";
            return View();
        }
    }
}
