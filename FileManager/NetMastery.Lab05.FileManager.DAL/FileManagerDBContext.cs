using System.Data.Entity;
using NetMastery.Lab05.FileManager.DAL.Entities;
using NetMastery.Lab05.FileManager.DAL.Configurations;

namespace NetMastery.Lab05.FileManager.DAL
{
    public class FileManagerDbContext : DbContext
    {
        public FileManagerDbContext() : base("name=FileManagerDB")
        {
            Database.SetInitializer(new FileManegerInitializer());
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<File> Files { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new AccountsMap());
            modelBuilder.Configurations.Add(new FileMap());
            modelBuilder.Configurations.Add(new StorageMap());
        }
    }
}
