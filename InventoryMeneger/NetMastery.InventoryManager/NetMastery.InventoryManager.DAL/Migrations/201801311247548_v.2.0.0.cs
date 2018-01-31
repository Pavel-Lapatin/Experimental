namespace NetMastery.InventoryManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v200 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InventoryInCards", "CardId", "dbo.Cards");
            DropForeignKey("dbo.InventoryInCards", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.Subdivisions", "Organization_OrganizationId", "dbo.Organizations");
            DropIndex("dbo.InventoryInCards", new[] { "CardId" });
            DropIndex("dbo.InventoryInCards", new[] { "InventoryId" });
            DropIndex("dbo.Subdivisions", new[] { "Organization_OrganizationId" });
            RenameColumn(table: "dbo.Subdivisions", name: "Organization_OrganizationId", newName: "OrganizationId");
            AddColumn("dbo.Inventories", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Inventories", "CardId", c => c.Int(nullable: false));
            AddColumn("dbo.Storages", "InventoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Organizations", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Organizations", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Subdivisions", "OrganizationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Subdivisions", "OrganizationId");
            CreateIndex("dbo.Storages", "InventoryId");
            CreateIndex("dbo.Inventories", "CardId");
            AddForeignKey("dbo.Inventories", "CardId", "dbo.Cards", "CardId", cascadeDelete: true);
            AddForeignKey("dbo.Storages", "InventoryId", "dbo.Inventories", "InventoryId", cascadeDelete: true);
            AddForeignKey("dbo.Subdivisions", "OrganizationId", "dbo.Organizations", "OrganizationId", cascadeDelete: true);
            DropColumn("dbo.Cards", "ResidiualCost");
            DropColumn("dbo.Organizations", "MimeType");
            DropColumn("dbo.Subdivisions", "Address");
            DropTable("dbo.InventoryInCards");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InventoryInCards",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        InventoryId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CardId, t.InventoryId });
            
            AddColumn("dbo.Subdivisions", "Address", c => c.String());
            AddColumn("dbo.Organizations", "MimeType", c => c.String());
            AddColumn("dbo.Cards", "ResidiualCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Subdivisions", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Storages", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.Inventories", "CardId", "dbo.Cards");
            DropIndex("dbo.Inventories", new[] { "CardId" });
            DropIndex("dbo.Storages", new[] { "InventoryId" });
            DropIndex("dbo.Subdivisions", new[] { "OrganizationId" });
            AlterColumn("dbo.Subdivisions", "OrganizationId", c => c.Int());
            AlterColumn("dbo.Organizations", "Address", c => c.String());
            AlterColumn("dbo.Organizations", "Name", c => c.String());
            DropColumn("dbo.Storages", "InventoryId");
            DropColumn("dbo.Inventories", "CardId");
            DropColumn("dbo.Inventories", "Quantity");
            RenameColumn(table: "dbo.Subdivisions", name: "OrganizationId", newName: "Organization_OrganizationId");
            CreateIndex("dbo.Subdivisions", "Organization_OrganizationId");
            CreateIndex("dbo.InventoryInCards", "InventoryId");
            CreateIndex("dbo.InventoryInCards", "CardId");
            AddForeignKey("dbo.Subdivisions", "Organization_OrganizationId", "dbo.Organizations", "OrganizationId");
            AddForeignKey("dbo.InventoryInCards", "InventoryId", "dbo.Inventories", "InventoryId", cascadeDelete: true);
            AddForeignKey("dbo.InventoryInCards", "CardId", "dbo.Cards", "CardId", cascadeDelete: true);
        }
    }
}
