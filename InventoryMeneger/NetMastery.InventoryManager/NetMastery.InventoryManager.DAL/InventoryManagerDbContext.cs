using Domain;
using System.Data.Entity;

namespace NetMastery.InventoryManager.DAL
{
    public class InventoryManagerDbContext : DbContext
    {
        public InventoryManagerDbContext() : base("name=InventoryManagerConnection")
        {
        }
        public InventoryManagerDbContext(string connectionString) : base(connectionString)
        {
        }
        public DbSet<Card> Accounts { get; set; }
        public DbSet<Inventory> Directories { get; set; }
        public DbSet<InventoryType> Files { get; set; }
        public DbSet<InventoryInCard> InventoryInCards { get; set; }
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<PersonInCharge> PersonInChargies { get; set; }
        public DbSet<Storage> Storagies { get; set; }
        public DbSet<Subdivision> Subdivisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Configurations.Add(new AccountsMap());
            //modelBuilder.Configurations.Add(new FileStructMap());
            //modelBuilder.Configurations.Add(new DirectoryStructMap());

        }
    }
}
