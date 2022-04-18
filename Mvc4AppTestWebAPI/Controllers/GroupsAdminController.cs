using Mvc4AppTestWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using com.minsoehanwin.sample.Services.AspNetIdentity;
using com.minsoehanwin.sample.Core.AspNetIdentity.Models;
using com.minsoehanwin.sample.Repositories.EF;

namespace Mvc4AppTestWebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupsAdminController : Controller
    {
        public GroupsAdminController()
        {
            ViewBag.ActiveManage = "active";
            ViewBag.ActiveGroupsAdmin = "active";
        }

        private MyDataContext _db;
        public MyDataContext db { 
            get 
            {
                return _db ?? HttpContext.GetOwinContext().Get<MyDataContext>();
            } 
            set 
            {
                _db = value;
            } 
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Roles/
        public ActionResult Index()
        {
            return View(UserManager.GetGroupList());
        }

        //
        // GET: /Roles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group = await UserManager.GetGroupByIdAsync(id);
            // Get the list of Users in this Group
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Group
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInGroupAsync(user.Id, group.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            var groupViewModel = new GroupViewModel() {Id=group.Id,Name=group.Name  };
            return View(groupViewModel);
        }

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(GroupViewModel groupViewModel)
        {
            if (ModelState.IsValid)
            {
                var group = new Group(groupViewModel.Name);
                var groupResult = await UserManager.CreateGroupAsync(group.Name);
                if (!groupResult.Succeeded)
                {
                    ModelState.AddModelError("", groupResult.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Roles/Edit/Admin
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group = await UserManager.GetGroupByIdAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            GroupViewModel groupViewModel = new GroupViewModel { Id = group.Id, Name = group.Name };

            ICollection<ApplicationRoleGroup> roleGroups = UserManager.GetGroupRoles(group.Id);

            return View(new EditGroupViewModel()
            {
                Id = group.Id,
                Name =group.Name,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = roleGroups.Where(z=>z.RoleId==x.Id).Count()==1,
                    Text = x.Name,
                    Value = x.Name
                })
            });
            return View(groupViewModel);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id")] GroupViewModel groupViewModel, params string[] selectedRole)
        {
            var tran=db.Database.BeginTransaction();
            try{
                if (ModelState.IsValid)
                {
                    var group = await UserManager.GetGroupByIdAsync(groupViewModel.Id);
                    await UserManager.UpdateGroupAsync(groupViewModel.Id, groupViewModel.Name);
                    UserManager.ClearGroupRoles(groupViewModel.Id);
                    foreach(var r in selectedRole)
                    {
                        UserManager.AddRoleToGroup(group.Name, r);
                    }
                    return RedirectToAction("Index");
                }
                db.SaveChanges();
                tran.Commit();
            }catch(System.Exception  ex)
            {
                tran.Rollback();
                throw;
            }
            return View();
        }

        //
        // GET: /Roles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group = await UserManager.GetGroupByIdAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(new GroupViewModel() { Id = group.Id, Name = group.Name });
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var group = await UserManager.GetGroupByIdAsync(id);
                    if (group == null)
                    {
                        return HttpNotFound();
                    }
                    UserManager.DeleteGroup(id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                    ModelState.AddModelError("", ex.Message);
            }
            return View();
        }
    }
}