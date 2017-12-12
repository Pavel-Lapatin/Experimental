using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.DAL.Entities;

namespace NetMastery.Lab05.FileManager.DAL
{
    public class FileManagerDBContext : DbContext
    {
        public FileManagerDBContext() : base("name=FileManagerDB")
        {
            Database.SetInitializer(new FileManegerInitializer());
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasKey(c => c.AccoountId);
            modelBuilder.Entity<Account>()
                .Property(p => p.Login)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode();
            modelBuilder.Entity<Account>()
                .Property(p => p.Password)
                .IsRequired();
            modelBuilder.Entity<Account>()
                .Property(p => p.CreationDate)
                .IsRequired();


        }
    }
}
