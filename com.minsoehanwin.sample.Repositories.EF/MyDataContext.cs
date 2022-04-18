using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.minsoehanwin.sample.Core.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using com.minsoehanwin.sample.Repositories.EF.EntityMap;
using System.Data.Entity.Infrastructure.Interception;
using com.minsoehanwin.sample.Core.EmailEntity;
using com.minsoehanwin.sample.Core.AspNetIdentity.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace com.minsoehanwin.sample.Repositories.EF
{
    public class MyDataContext : IdentityDbContext<ApplicationUser>
    {
        public MyDataContext()
            : base("Name=MyDataContext",throwIfV1Schema: false)
        {
            //http://www.entityframeworktutorial.net/code-first/database-initialization-strategy-in-code-first.aspx
            //Database.SetInitializer(new CreateDatabaseIfNotExists<MyDataContext>();
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyDataContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<MyDataContext>());
            //Database.SetInitializer<MyDataContext>(new MyCustomDBInitializer());
            //Database.ExecuteSqlCommand("TRUNCATE TABLE PassportInfo");
            //Database.ExecuteSqlCommand("TRUNCATE TABLE Wife");
            //Database.ExecuteSqlCommand("TRUNCATE TABLE Product");
            //Database.ExecuteSqlCommand("TRUNCATE TABLE StoreProduct");
            //Database.ExecuteSqlCommand("TRUNCATE TABLE Employee");
            //Database.ExecuteSqlCommand("TRUNCATE TABLE Store");

            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            // Database.SetInitializer<MyDataContext>(new ApplicationDbInitializer());
        }
        public static MyDataContext Create()
        {
            return new MyDataContext();
        }

        public IDbSet<Group> Groups { get; set; }
        public IDbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        public IDbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<PassportInfo> PassportInfos { get; set; }
        public DbSet<Wife> Wifes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailTo> EmailTos { get; set; }
        public DbSet<EmailCc> EmailCcs { get; set; }
        public DbSet<EmailBcc> EmailBccs { get; set; }
        public DbSet<EmailAttachment> EmailAttachments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }
            modelBuilder.Entity<ApplicationUserGroup>()
                            .HasKey(ug => new { UserId = ug.UserId, GroupId = ug.GroupId })
                            .ToTable("ApplicationUserGroups");

            modelBuilder.Entity<ApplicationRoleGroup>()
                .HasKey(rg => new { RoleId = rg.RoleId, GroupId = rg.GroupId })
                .ToTable("ApplicationRoleGroups");

            modelBuilder.Entity<Group>()
                .ToTable("Groups")
                .HasKey(t => t.Id)
                .HasMany<ApplicationRoleGroup>(rg => rg.Roles)
                .WithRequired(g => g.Group)
                .HasForeignKey(fk => fk.GroupId);

            
            modelBuilder.Configurations.Add(new EmployeeEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
            modelBuilder.Configurations.Add(new StoreEntityConfiguration());
            modelBuilder.Configurations.Add(new PassportInfoEntityConfiguration());
            modelBuilder.Configurations.Add(new WifeEntityConfiguration());
            modelBuilder.Configurations.Add(new CarEntityConfiguration());
            modelBuilder.Configurations.Add(new EmailEntityConfiguration());
            modelBuilder.Configurations.Add(new EmailToEntityConfiguration());
            modelBuilder.Configurations.Add(new EmailCcEntityConfiguration());
            modelBuilder.Configurations.Add(new EmailBccEntityConfiguration());
            modelBuilder.Configurations.Add(new EmailAttachmentEntityConfiguration());
            //modelBuilder.Configurations.Add(new StoreProductConfiguration());
            //http://www.entityframeworktutorial.net/entityframework6/code-first-insert-update-delete-stored-procedure-mapping.aspx
            //modelBuilder.Entity<Student>()
            //.MapToStoredProcedures();
            //OR
            //modelBuilder.Entity<Student>()
            //.MapToStoredProcedures(p => p.Insert(sp => sp.HasName("sp_InsertStudent").Parameter(pm => pm.StudentName, "name").Result(rs => rs.Student_ID, "Student_ID"))
            //.Update(sp => sp.HasName("sp_UpdateStudent").Parameter(pm => pm.StudentName, "name"))
            //.Delete(sp => sp.HasName("sp_DeleteStudent").Parameter(pm => pm.Student_ID, "Id"))
            //);
            //OR
            //modelBuilder.Types().Configure(t => t.MapToStoredProcedures());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Auto add Audit info to Entities
        /// see more at http://stackoverflow.com/questions/26355486/entity-framework-6-audit-track-changes
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var addedAuditedEntities = ChangeTracker.Entries<AuditableEntity>()
              .Where(p => p.State == EntityState.Added)
              .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<AuditableEntity>()
              .Where(p => p.State == EntityState.Modified)
              .Select(p => p.Entity);

            var now = DateTime.UtcNow;

            foreach (var added in addedAuditedEntities)
            {
                added.CreatedDate = now;
                //added.UpdatedDate = now;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.UpdatedDate = now;
            }
            //http://stackoverflow.com/questions/1331779/conversion-of-a-datetime2-data-type-to-a-datetime-data-type-results-out-of-range
            return base.SaveChanges();
        }
    }
}