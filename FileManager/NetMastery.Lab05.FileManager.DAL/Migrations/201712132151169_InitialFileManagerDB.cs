namespace NetMastery.Lab05.FileManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialFileManagerDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccoountId = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        RootDirectory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccoountId)
                .ForeignKey("dbo.DirectoryInfoes", t => t.RootDirectory)
                .Index(t => t.Login, unique: true, name: "UQ_Account_Name")
                .Index(t => t.RootDirectory);
            
            CreateTable(
                "dbo.DirectoryInfoes",
                c => new
                    {
                        StorageId = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Name = c.String(nullable: false, maxLength: 255),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ParentStorageId = c.Int(),
                    })
                .PrimaryKey(t => t.StorageId)
                .ForeignKey("dbo.DirectoryInfoes", t => t.ParentStorageId)
                .Index(t => t.ParentStorageId);
            
            CreateTable(
                "dbo.FileInfoes",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        FileSize = c.Int(nullable: false),
                        DownloadsNumber = c.Int(nullable: false),
                        FileTypeId = c.Int(nullable: false),
                        StorageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.FileTypes", t => t.FileTypeId, cascadeDelete: true)
                .ForeignKey("dbo.DirectoryInfoes", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.FileTypeId)
                .Index(t => t.StorageId);
            
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
            DropForeignKey("dbo.Accounts", "RootDirectory", "dbo.DirectoryInfoes");
            DropForeignKey("dbo.DirectoryInfoes", "ParentStorageId", "dbo.DirectoryInfoes");
            DropForeignKey("dbo.FileInfoes", "StorageId", "dbo.DirectoryInfoes");
            DropForeignKey("dbo.FileInfoes", "FileTypeId", "dbo.FileTypes");
            DropIndex("dbo.FileTypes", "UQ_FileType_Extension");
            DropIndex("dbo.FileInfoes", new[] { "StorageId" });
            DropIndex("dbo.FileInfoes", new[] { "FileTypeId" });
            DropIndex("dbo.DirectoryInfoes", new[] { "ParentStorageId" });
            DropIndex("dbo.Accounts", new[] { "RootDirectory" });
            DropIndex("dbo.Accounts", "UQ_Account_Name");
            DropTable("dbo.FileTypes");
            DropTable("dbo.FileInfoes");
            DropTable("dbo.DirectoryInfoes");
            DropTable("dbo.Accounts");
        }
    }
}
