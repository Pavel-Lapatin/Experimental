namespace NetMastery.InventoryManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v100 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        FullCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WearCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ResidiualCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CardId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        InventoryNumber = c.String(nullable: false),
                        SerialNumber = c.String(nullable: false),
                        PassportNumber = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        CreationTime = c.String(),
                        Model = c.String(),
                        Status = c.Int(nullable: false),
                        InventoryTypeId = c.Int(nullable: false),
                        ManufactureId = c.Int(),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.InventoryTypes", t => t.InventoryTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Manufactures", t => t.ManufactureId)
                .Index(t => t.InventoryTypeId)
                .Index(t => t.ManufactureId);
            
            CreateTable(
                "dbo.InventoryTypes",
                c => new
                    {
                        InventoryTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AssetDepricationLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryTypeId);
            
            CreateTable(
                "dbo.Manufactures",
                c => new
                    {
                        ManufactureId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ManufactureId);
            
            CreateTable(
                "dbo.InventoryInCards",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        InventoryId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CardId, t.InventoryId })
                .ForeignKey("dbo.Cards", t => t.CardId, cascadeDelete: true)
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .Index(t => t.CardId)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        OrganizationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        image = c.Binary(),
                        MimeType = c.String(),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrganizationId)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AccountId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
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
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Subdivisions",
                c => new
                    {
                        SubdivisionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        Organization_OrganizationId = c.Int(),
                    })
                .PrimaryKey(t => t.SubdivisionId)
                .ForeignKey("dbo.Organizations", t => t.Organization_OrganizationId)
                .Index(t => t.Organization_OrganizationId);
            
            CreateTable(
                "dbo.PersonInCharges",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Position = c.String(),
                        SubdivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Subdivisions", t => t.SubdivisionId, cascadeDelete: true)
                .Index(t => t.SubdivisionId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        StorageId = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        PersonInChargeId = c.Int(nullable: false),
                        PersonInCharge_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.StorageId)
                .ForeignKey("dbo.PersonInCharges", t => t.PersonInCharge_PersonId)
                .Index(t => t.PersonInCharge_PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Storages", "PersonInCharge_PersonId", "dbo.PersonInCharges");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Subdivisions", "Organization_OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.PersonInCharges", "SubdivisionId", "dbo.Subdivisions");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Organizations", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.InventoryInCards", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.InventoryInCards", "CardId", "dbo.Cards");
            DropForeignKey("dbo.Inventories", "ManufactureId", "dbo.Manufactures");
            DropForeignKey("dbo.Inventories", "InventoryTypeId", "dbo.InventoryTypes");
            DropIndex("dbo.Storages", new[] { "PersonInCharge_PersonId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PersonInCharges", new[] { "SubdivisionId" });
            DropIndex("dbo.Subdivisions", new[] { "Organization_OrganizationId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "AccountId" });
            DropIndex("dbo.Organizations", new[] { "AccountId" });
            DropIndex("dbo.InventoryInCards", new[] { "InventoryId" });
            DropIndex("dbo.InventoryInCards", new[] { "CardId" });
            DropIndex("dbo.Inventories", new[] { "ManufactureId" });
            DropIndex("dbo.Inventories", new[] { "InventoryTypeId" });
            DropTable("dbo.Storages");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PersonInCharges");
            DropTable("dbo.Subdivisions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Accounts");
            DropTable("dbo.Organizations");
            DropTable("dbo.InventoryInCards");
            DropTable("dbo.Manufactures");
            DropTable("dbo.InventoryTypes");
            DropTable("dbo.Inventories");
            DropTable("dbo.Cards");
        }
    }
}
