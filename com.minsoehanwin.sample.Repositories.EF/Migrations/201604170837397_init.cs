namespace com.minsoehanwin.sample.Repositories.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationRoleGroups",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.GroupId })
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        EmployeeId = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        FirstName = c.String(maxLength: 50),
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Zipcode = c.Int(),
                        State = c.String(),
                        Country = c.String(),
                        MyStoreId = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Store", t => t.MyStoreId)
                .Index(t => t.MyStoreId);
            
            CreateTable(
                "dbo.PassportInfo",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        PassportNo = c.String(),
                        IssueDate = c.DateTime(nullable: false),
                        ExpiredDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wife",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmployeeId = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmailAttachment",
                c => new
                    {
                        EmailId = c.String(nullable: false, maxLength: 128),
                        FileName = c.String(nullable: false, maxLength: 128),
                        PhysicalFilePath = c.String(),
                    })
                .PrimaryKey(t => new { t.EmailId, t.FileName })
                .ForeignKey("dbo.Email", t => t.EmailId, cascadeDelete: true)
                .Index(t => t.EmailId);
            
            CreateTable(
                "dbo.Email",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Reference = c.String(),
                        Comment = c.String(),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ExpectedDeliveryDateTime = c.DateTime(),
                        ActualDeliveryDateTime = c.DateTime(),
                        DeliveredBy = c.String(),
                        From = c.String(),
                        FromName = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailBcc",
                c => new
                    {
                        EmailId = c.String(nullable: false, maxLength: 128),
                        EmailAddress = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.EmailId, t.EmailAddress })
                .ForeignKey("dbo.Email", t => t.EmailId, cascadeDelete: true)
                .Index(t => t.EmailId);
            
            CreateTable(
                "dbo.EmailCc",
                c => new
                    {
                        EmailId = c.String(nullable: false, maxLength: 128),
                        EmailAddress = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.EmailId, t.EmailAddress })
                .ForeignKey("dbo.Email", t => t.EmailId, cascadeDelete: true)
                .Index(t => t.EmailId);
            
            CreateTable(
                "dbo.EmailTo",
                c => new
                    {
                        EmailId = c.String(nullable: false, maxLength: 128),
                        EmailAddress = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.EmailId, t.EmailAddress })
                .ForeignKey("dbo.Email", t => t.EmailId, cascadeDelete: true)
                .Index(t => t.EmailId);
            
            CreateTable(
                "dbo.StoreProduct",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.StoreId })
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailTo", "EmailId", "dbo.Email");
            DropForeignKey("dbo.EmailCc", "EmailId", "dbo.Email");
            DropForeignKey("dbo.EmailBcc", "EmailId", "dbo.Email");
            DropForeignKey("dbo.EmailAttachment", "EmailId", "dbo.Email");
            DropForeignKey("dbo.Wife", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "MyStoreId", "dbo.Store");
            DropForeignKey("dbo.StoreProduct", "StoreId", "dbo.Store");
            DropForeignKey("dbo.StoreProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.PassportInfo", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Car", "Id", "dbo.Employee");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.Groups");
            DropIndex("dbo.StoreProduct", new[] { "StoreId" });
            DropIndex("dbo.StoreProduct", new[] { "ProductId" });
            DropIndex("dbo.EmailTo", new[] { "EmailId" });
            DropIndex("dbo.EmailCc", new[] { "EmailId" });
            DropIndex("dbo.EmailBcc", new[] { "EmailId" });
            DropIndex("dbo.EmailAttachment", new[] { "EmailId" });
            DropIndex("dbo.Wife", new[] { "EmployeeId" });
            DropIndex("dbo.PassportInfo", new[] { "EmployeeId" });
            DropIndex("dbo.Employee", new[] { "MyStoreId" });
            DropIndex("dbo.Car", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ApplicationUserGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropTable("dbo.StoreProduct");
            DropTable("dbo.EmailTo");
            DropTable("dbo.EmailCc");
            DropTable("dbo.EmailBcc");
            DropTable("dbo.Email");
            DropTable("dbo.EmailAttachment");
            DropTable("dbo.Wife");
            DropTable("dbo.Product");
            DropTable("dbo.Store");
            DropTable("dbo.PassportInfo");
            DropTable("dbo.Employee");
            DropTable("dbo.Car");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Groups");
            DropTable("dbo.ApplicationRoleGroups");
        }
    }
}
