namespace NetMastery.Lab05.FileManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v100 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        RootDirectory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.DirectoryStructures", t => t.RootDirectory)
                .Index(t => t.Login, unique: true)
                .Index(t => t.RootDirectory);
            
            CreateTable(
                "dbo.DirectoryStructures",
                c => new
                    {
                        DirectoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        FullPath = c.String(),
                        ParantDirectory = c.Int(),
                    })
                .PrimaryKey(t => t.DirectoryId)
                .ForeignKey("dbo.DirectoryStructures", t => t.ParantDirectory)
                .Index(t => t.ParantDirectory);
            
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
                        Directory = c.Int(nullable: false),
                        FileType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.DirectoryStructures", t => t.Directory, cascadeDelete: true)
                .ForeignKey("dbo.FileTypes", t => t.FileType, cascadeDelete: true)
                .Index(t => t.Directory)
                .Index(t => t.FileType);
            
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
            DropForeignKey("dbo.Accounts", "RootDirectory", "dbo.DirectoryStructures");
            DropForeignKey("dbo.DirectoryStructures", "ParantDirectory", "dbo.DirectoryStructures");
            DropForeignKey("dbo.FileStructures", "FileType", "dbo.FileTypes");
            DropForeignKey("dbo.FileStructures", "Directory", "dbo.DirectoryStructures");
            DropIndex("dbo.FileTypes", "UQ_FileType_Extension");
            DropIndex("dbo.FileStructures", new[] { "FileType" });
            DropIndex("dbo.FileStructures", new[] { "Directory" });
            DropIndex("dbo.DirectoryStructures", new[] { "ParantDirectory" });
            DropIndex("dbo.Accounts", new[] { "RootDirectory" });
            DropIndex("dbo.Accounts", new[] { "Login" });
            DropTable("dbo.FileTypes");
            DropTable("dbo.FileStructures");
            DropTable("dbo.DirectoryStructures");
            DropTable("dbo.Accounts");
        }
    }
}
