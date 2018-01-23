using NetMastery.InventoryManager.Domain;
using NetMastery.InventoryManager.DAL.DbConfiguration;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace NetMastery.InventoryManager.DAL
{
    public class InventoryDbContext : IdentityDbContext<User>
    {
        public InventoryDbContext() 
            : base("name=InventoryManagerConnection", throwIfV1Schema: false)
        {
        }
        public InventoryDbContext(string connectionString) : base(connectionString)
        {
        }

        public static InventoryDbContext Create()
        {
            var context = new InventoryDbContext();
            CreateRoles(context);
            return new InventoryDbContext();
        }
        public IDbSet<Card> Cards { get; set; }
        public IDbSet<Inventory> Directories { get; set; }
        public IDbSet<InventoryType> Files { get; set; }
        public IDbSet<InventoryInCard> InventoryInCards { get; set; }
        public IDbSet<Manufacture> Manufactures { get; set; }
        public IDbSet<PersonInCharge> PersonInChargies { get; set; }
        public IDbSet<Storage> Storagies { get; set; }
        public IDbSet<Subdivision> Subdivisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new InventoryInCardMap());
            modelBuilder.Configurations.Add(new PersonInChargeMap());
            modelBuilder.Configurations.Add(new UserMap());
            base.OnModelCreating(modelBuilder);
        }

        public static void CreateRoles(InventoryDbContext context)
        {
            RoleManager<Role> roleManager = new RoleManager<Role>(new RoleStore<Role>(context));
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));
            Role roleAdmin, roleUser = null;
            User userAdmin = null;
            // поиск роли admin
            roleAdmin = roleManager.FindByName("admin");
            if (roleAdmin == null)
            {
                // создаем, если на нашли
                roleAdmin = new Role { Name = "admin" };
                var result = roleManager.Create(roleAdmin);
            }
            // поиск роли user
            roleUser = roleManager.FindByName("manager");
            if (roleUser == null)
            {
                // создаем, если на нашли
                roleUser = new Role { Name = "manager" };
                var result = roleManager.Create(roleUser);
            }
        }
    }
}
