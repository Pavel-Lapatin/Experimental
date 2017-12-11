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
    public class FileManagerDb : DbContext
    {
        public FileManagerDb(string name) : base(name)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasRequired(c => new {c.Login, c.Password, c.CreationDate});
            modelBuilder.Entity<Storage>()
                .HasKey(c => c.)
        }
    }
}
