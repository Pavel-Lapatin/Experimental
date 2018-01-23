using NetMastery.InventoryManager.Domain;
using NetMastery.InventoryManager.DAL.DbConfiguration;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace NetMastery.InventoryManager.DAL
{
    public class InventoryManagerDbContext : IdentityDbContext<User>
    {
        public InventoryManagerDbContext() 
            : base("name=InventoryManagerConnection", throwIfV1Schema: false)
        {
        }
        public InventoryManagerDbContext(string connectionString) : base(connectionString)
        {
        }

        public static InventoryManagerDbContext Create()
        {
            return new InventoryManagerDbContext();
        }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Inventory> Directories { get; set; }
        public DbSet<InventoryType> Files { get; set; }
        public DbSet<InventoryInCard> InventoryInCards { get; set; }
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<PersonInCharge> PersonInChargies { get; set; }
        public DbSet<Storage> Storagies { get; set; }
        public DbSet<Subdivision> Subdivisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new InventoryInCardMap());
            modelBuilder.Configurations.Add(new PersonInChargeMap());
            modelBuilder.Configurations.Add(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
