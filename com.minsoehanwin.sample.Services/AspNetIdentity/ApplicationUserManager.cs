using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.minsoehanwin.sample.Repositories.EF;
using com.minsoehanwin.sample.Core.AspNetIdentity.Models;

namespace com.minsoehanwin.sample.Services.AspNetIdentity
{
    // Configure the application user manager used in this application. 
    // UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        // Swap ApplicationRole for IdentityRole:
        private ApplicationRoleManager _applicationRoleManager = null;
        private MyDataContext _db;

        public ApplicationUserManager(IUserStore<ApplicationUser> store, MyDataContext db, ApplicationRoleManager applicationRoleManager)
            : base(store)
        {
            _db = db;
            _applicationRoleManager = applicationRoleManager;
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var db = context.Get<MyDataContext>();
            var roleManager = context.Get<ApplicationRoleManager>();
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db), db, roleManager);
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public bool RoleExists(string name)
        {
            return _applicationRoleManager.RoleExists(name);
        }

        public bool CreateRole(string name, string description = "")
        {
            // Swap ApplicationRole for IdentityRole:
            var idResult = _applicationRoleManager.Create(new ApplicationRole(name, description));
            return idResult.Succeeded;
        }

        public bool CreateUser(ApplicationUser user, string password)
        {
            var idResult = this.Create(user, password);
            return idResult.Succeeded;
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = this.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        public void ClearUserRoles(string userId)
        {
            var user = this.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                var roleName = _applicationRoleManager.FindById(role.RoleId).Name;
                this.RemoveFromRole(userId, roleName);
            }
        }

        public void CreateGroup(string groupName)
        {
            if (this.GroupNameExists(groupName))
            {
                throw new System.Exception("A group by that name already exists in the database. Please choose another name.");
            }

            var newGroup = new Group(groupName);
            _db.Groups.Add(newGroup);
            _db.SaveChanges();
        }

        public async Task<GroupResult> CreateGroupAsync(string groupName)
        {
            try
            {
                CreateGroup(groupName);
                var group = await GetGroupByNameAsync(groupName);
                if (group != null)
                {
                    return new GroupResult() { Succeeded = true };
                }
                return new GroupResult() { Succeeded = false, Errors = new List<string>() { "No group found." } };
            }
            catch (System.Exception ex)
            {
                return new GroupResult() { Succeeded = false, Errors = new List<string>() { ex.Message } };
            }
        }

        public async Task<Group> GetGroupByNameAsync(string groupName)
        {
            return _db.Groups.Where(x => x.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }
        public async Task<Group> GetGroupByIdAsync(string id)
        {
            return _db.Groups.Where(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        public bool GroupNameExists(string groupName)
        {
            var g = _db.Groups.Where(gr => gr.Name == groupName);
            if (g.Count() > 0)
            {
                return true;
            }
            return false;
        }
        public ICollection<Group> GetGroupList()
        {
            return _db.Groups.ToList();
        }
        public ICollection<ApplicationRoleGroup> GetGroupRoles(string groupId)
        {
            return _db.Groups.Where(x => x.Id == groupId).FirstOrDefault().Roles.ToList();
        }
        public void ClearUserGroupsByName(string userName)
        {
            var user = _db.Users.Where(x => x.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            ClearUserGroups(user.Id);
        }

        public void ClearUserGroups(string userId)
        {
            this.ClearUserRoles(userId);
            var user = _db.Users.Find(userId);
            user.Groups.Clear();
            _db.SaveChanges();
        }

        public void AddUserToGroupById(string userId, string GroupId)
        {
            var group = _db.Groups.Find(GroupId);
            var user = _db.Users.Find(userId);

            var userGroup = new ApplicationUserGroup()
            {
                Group = group,
                GroupId = group.Id,
                User = user,
                UserId = user.Id
            };

            foreach (var role in group.Roles)
            {
                this.AddToRole(userId, role.Role.Name);
            }
            user.Groups.Add(userGroup);
            _db.SaveChanges();
        }

        public void ClearGroupRoles(string groupId)
        {
            var group = _db.Groups.Find(groupId);
            var groupUsers = _db.Users.Where(u => u.Groups.Any(g => g.GroupId == group.Id));

            foreach (var role in group.Roles)
            {
                var currentRoleId = role.RoleId;
                foreach (var user in groupUsers)
                {
                    // Is the user a member of any other groups with this role?
                    var groupsWithRole = user.Groups
                        .Where(g => g.Group.Roles
                            .Any(r => r.RoleId == currentRoleId)).Count();
                    // This will be 1 if the current group is the only one:
                    if (groupsWithRole == 1)
                    {
                        this.RemoveFromRole(user.Id, role.Role.Name);
                    }
                }
            }
            group.Roles.Clear();
            _db.SaveChanges();
        }

        public void AddRoleToGroup(string groupName, string roleName)
        {
            var group = _db.Groups.Where(x => x.Name == groupName).FirstOrDefault();
            AddRoleToGroupById(group.Id, roleName);
        }

        public void AddRoleToGroupById(string groupId, string roleName)
        {
            var group = _db.Groups.Find(groupId);
            var role = _db.Roles.First(r => r.Name == roleName);
            var newgroupRole = new ApplicationRoleGroup()
            {
                GroupId = group.Id,
                Group = group,
                RoleId = role.Id,
                Role = role
            };
            group.Roles.Add(newgroupRole);
            _db.SaveChanges();

            // Add all of the users in this group to the new role:
            var groupUsers = _db.Users.Where(u => u.Groups.Any(g => g.GroupId == group.Id));
            foreach (var user in groupUsers)
            {
                if (!(this.IsInRole(user.Id, roleName)))
                {
                    this.AddToRole(user.Id, roleName);
                }
            }
        }

        public void DeleteGroup(string groupId)
        {
            var group = _db.Groups.Find(groupId);

            // Clear the roles from the group:
            this.ClearGroupRoles(groupId);
            _db.Groups.Remove(group);
            _db.SaveChanges();
        }

        public void AddUserToGroup(string userName, string[] groupNames)
        {
            foreach (var gName in groupNames)
            {
                AddUserToGroup(userName, gName);
            }
        }

        public void AddUserToGroup(string userName, string groupName)
        {
            var user = this.FindByName(userName);
            var group = _db.Groups.Where(x => x.Name == groupName).FirstOrDefault();
            AddUserToGroupById(user.Id, group.Id);
        }

        public async Task<bool> IsInGroupAsync(string userId, string groupName)
        {
            var user = await FindByIdAsync(userId);
            var g = user.Groups.Where(x => x.Group.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault<ApplicationUserGroup>();
            return g != null;
        }



        public async Task UpdateGroupAsync(string id, string name)
        {
            var g = await GetGroupByIdAsync(id);
            g.Name = name;
            await _db.SaveChangesAsync();
        }
    }

}
