namespace NetMastery.Lab05.FileManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v101 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccoountId = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccoountId)
                .ForeignKey("dbo.DirectoryStructures", t => t.AccoountId)
                .Index(t => t.AccoountId)
                .Index(t => t.Login, unique: true, name: "UQ_Account_Name");
            
            CreateTable(
                "dbo.DirectoryStructures",
                c => new
                    {
                        DirectoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ParentDirectoryId = c.Int(),
                    })
                .PrimaryKey(t => t.DirectoryId)
                .ForeignKey("dbo.DirectoryStructures", t => t.ParentDirectoryId)
                .Index(t => t.ParentDirectoryId);
            
            CreateTable(
                "dbo.FileStructures",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        FileSize = c.Int(nullable: false),
                        DownloadsNumber = c.Int(nullable: false),
                        FileTypeId = c.Int(nullable: false),
                        DirectoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.DirectoryStructures", t => t.DirectoryId, cascadeDelete: true)
                .ForeignKey("dbo.FileTypes", t => t.FileTypeId, cascadeDelete: true)
                .Index(t => t.FileTypeId)
                .Index(t => t.DirectoryId);
            
            CreateTable(
                "dbo.FileTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Extension = c.String(nullable: false, maxLength: 20),
                        RelatedProgram = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.TypeId)
                .Index(t => t.Extension, unique: true, name: "UQ_FileType_Extension");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DirectoryStructures", "ParentDirectoryId", "dbo.DirectoryStructures");
            DropForeignKey("dbo.FileStructures", "FileTypeId", "dbo.FileTypes");
            DropForeignKey("dbo.FileStructures", "DirectoryId", "dbo.DirectoryStructures");
            DropForeignKey("dbo.Accounts", "AccoountId", "dbo.DirectoryStructures");
            DropIndex("dbo.FileTypes", "UQ_FileType_Extension");
            DropIndex("dbo.FileStructures", new[] { "DirectoryId" });
            DropIndex("dbo.FileStructures", new[] { "FileTypeId" });
            DropIndex("dbo.DirectoryStructures", new[] { "ParentDirectoryId" });
            DropIndex("dbo.Accounts", "UQ_Account_Name");
            DropIndex("dbo.Accounts", new[] { "AccoountId" });
            DropTable("dbo.FileTypes");
            DropTable("dbo.FileStructures");
            DropTable("dbo.DirectoryStructures");
            DropTable("dbo.Accounts");
        }
    }
}
