using System.Data.Entity;
using NetMastery.Lab05.FileManager.DAL.Configurations;
using NetMastery.Lab05.FileManager.Domain;

namespace NetMastery.Lab05.FileManager.DAL
{
    public class FileManagerDbContext : DbContext
    {
        public FileManagerDbContext() : base("name=FileManagerDB")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<DirectoryStructure> Directories { get; set; }
        public DbSet<FileStructure> Files { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new AccountsMap());
            modelBuilder.Configurations.Add(new FileStructMap());
            modelBuilder.Configurations.Add(new DirectoryStructMap());

        }
    }
}
