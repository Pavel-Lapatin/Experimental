namespace NetMastery.Lab05.FileManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueFullPath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DirectoryStructures", "FullPath", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.DirectoryStructures", "FullPath", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.DirectoryStructures", new[] { "FullPath" });
            AlterColumn("dbo.DirectoryStructures", "FullPath", c => c.String());
        }
    }
}
