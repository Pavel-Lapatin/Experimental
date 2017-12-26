using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.Database
{
    
    public class DatabaseInitialiser<TContext, TMigrationsConfiguration> : IDatabaseInitializer<TContext>
        where TContext : DbContext
        where TMigrationsConfiguration : DbMigrationsConfiguration<TContext>, new()
    {
        private readonly DbMigrationsConfiguration config;

        public DatabaseInitialiser()
        {
            config = new TMigrationsConfiguration();
        }

        public void InitializeDatabase(TContext context)
        {
            if (context == null)
            {
                throw new ArgumentException();
            }
            new DbMigrator(config).Update();
        }
    }
}
