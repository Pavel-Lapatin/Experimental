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
        public DbSet<DirectoryInfo> Directories { get; set; }
        public DbSet<FileInfo> Files { get; set; }
        public DbSet<FileType> FileTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new AccountsMap());
            modelBuilder.Configurations.Add(new FileMap());
            modelBuilder.Configurations.Add(new StorageMap());
            modelBuilder.Configurations.Add(new FileTypeMap());
        }
    }
}
