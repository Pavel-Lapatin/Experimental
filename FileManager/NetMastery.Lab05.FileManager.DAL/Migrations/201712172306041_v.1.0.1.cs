namespace NetMastery.Lab05.FileManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v101 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileStructures", "FileType", "dbo.FileTypes");
            DropIndex("dbo.FileStructures", new[] { "FileType" });
            DropIndex("dbo.FileTypes", "UQ_FileType_Extension");
            AddColumn("dbo.Accounts", "MaxStorageSize", c => c.Long(nullable: false));
            AddColumn("dbo.Accounts", "CurentStorageSize", c => c.Long(nullable: false));
            AddColumn("dbo.DirectoryStructures", "DirectorySize", c => c.Long(nullable: false));
            AddColumn("dbo.FileStructures", "Extension", c => c.String());
            AlterColumn("dbo.FileStructures", "FileSize", c => c.Long(nullable: false));
            DropColumn("dbo.FileStructures", "FileType");
            DropTable("dbo.FileTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Extension = c.String(nullable: false, maxLength: 20),
                        RelatedProgram = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.TypeId);
            
            AddColumn("dbo.FileStructures", "FileType", c => c.Int(nullable: false));
            AlterColumn("dbo.FileStructures", "FileSize", c => c.Int(nullable: false));
            DropColumn("dbo.FileStructures", "Extension");
            DropColumn("dbo.DirectoryStructures", "DirectorySize");
            DropColumn("dbo.Accounts", "CurentStorageSize");
            DropColumn("dbo.Accounts", "MaxStorageSize");
            CreateIndex("dbo.FileTypes", "Extension", unique: true, name: "UQ_FileType_Extension");
            CreateIndex("dbo.FileStructures", "FileType");
            AddForeignKey("dbo.FileStructures", "FileType", "dbo.FileTypes", "TypeId", cascadeDelete: true);
        }
    }
}
