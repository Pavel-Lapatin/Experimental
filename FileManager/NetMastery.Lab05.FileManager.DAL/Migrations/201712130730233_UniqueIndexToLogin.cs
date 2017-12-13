namespace NetMastery.Lab05.FileManager.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UniqueIndexToLogin : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Accounts", "Login", unique: true, name: "UQ_Account_Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Accounts", "UQ_Account_Name");
        }
    }
}
