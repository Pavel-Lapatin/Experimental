using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NetMastery.InventoryManager.Domain;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;

namespace NetMastery.InventoryManager.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NetMastery.InventoryManager.DAL.InventoryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NetMastery.InventoryManager.DAL.InventoryDbContext context)
        {
            var account = new Account { AccountId = 1 };
            if(context.Accounts.FirstOrDefault(x=>x.AccountId == 1) == null)
            {
                Debug.WriteLine("Writing in data base");
                context.Accounts.Add(account);
                context.SaveChanges();
            }
            RoleManager<Role> roleManager = new RoleManager<Role>(new RoleStore<Role>(context));
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));
            if (!roleManager.RoleExists("accountant"))
            {
                roleManager.Create(new Role { Name = "accountant" });
            }
            if (!roleManager.RoleExists("manager"))
            {
                roleManager.Create(new Role { Name = "manager" });
            }
            if (!roleManager.RoleExists("admin"))
            {
                roleManager.Create(new Role { Name = "admin" });
            }
            if (userManager.Find("Demo", "Demo_123") == null)
            {
                var user = new User
                {
                    UserName = "Demo",
                    AccountId = 1,
                    Email = "demo@gmail.com",
                    PhoneNumber = "+375292615084"
                };
                userManager.Create(user, "Demo_123");
                userManager.AddToRole(user.Id, "admin");
            }

            #region addOrganization
            var organisation = new[]
            {
                new Organization
                {
                    Name = "OOO \"TonnelNovacia\"",
                    Address = "Rebublic of Belarus, Minsk, Belskiy str., 85, office 45",
                    Email = "tonnelnovacia@gmail.com",
                    OrganizationId = 1,
                    Phone = "+375 29 261-50-84",
                    AccountId = 1
                },
                 new Organization
                {
                    Name = "OOO \"Belarusbank\"",
                    Address = "Rebublic of Belarus, Minsk, Surganova str., 12",
                    Email = "belorudbankgmail.com",
                    OrganizationId = 2,
                    Phone = "+375 29 25-78-96",
                    AccountId = 1
                },
                  new Organization
                {
                    Name = "OOO \"MAZ\"",
                    Address = "Rebublic of Belarus, Minsk, Partizanskiy prosp., 1",
                    Email = "maz@Gmail.com",
                    OrganizationId = 3,
                    Phone = "+375 29 24-87-36",
                    AccountId = 1
                },
                   new Organization
                {
                    Name = "OAO \"Kamunarka\"",
                    Address = "Rebublic of Belarus, Minsk, Pritickogo str., 11, office 32",
                    Email = "kamunarka@Gmail.com",
                    OrganizationId = 4,
                    Phone = "+375 29 261-50-84",
                    AccountId = 1
                },
                    new Organization
                {
                    Name = "OOO \"Kamunarka\", filial 2",
                    Address = "Rebublic of Belarus, Minsk, Pritickogo str., 8",
                    Email = "kamunarka@gmail.com",
                    OrganizationId = 5,
                    Phone = "+375 29 214-14-14",
                    AccountId = 1
                },
                     new Organization
                {
                    Name = "RUE \"BelNIIS\"",
                    Address = "Rebublic of Belarus, Minsk, F.Skorins str., 15B",
                    Email = "belniis@gmail.com",
                    OrganizationId = 6,
                    Phone = "+375 29 255-55-55",
                    AccountId = 1
                },
                      new Organization
                {
                    Name = "OOO \"GrodnoAzot\"",
                    Address = "Rebublic of Belarus, Grodno, Lenina str., 22",
                    Email = "grodnoazot@Gmail.com",
                    OrganizationId = 7,
                    Phone = "+375 29 222-22-22",
                    AccountId = 1
                },
                       new Organization
                {
                    Name = "OOO \"Teremok\"",
                    Address = "Rebublic of Belarus, Minsk, Belskiy str., 1, office 35",
                    Email = "teremok@Gmail.com",
                    OrganizationId = 8,
                    Phone = "+375 29 211-13-88",
                    AccountId = 1
                },
                        new Organization
                {
                    Name = "OOO \"NewLife\"",
                    Address = "Russia, Moscwa, Diatlova str., 22, office 22",
                    Email = "newLife@Gmail.com",
                    OrganizationId = 9,
                    Phone = "+375 29 260-17-21",
                    AccountId = 1
                }
            };

            context.Organizations.AddOrUpdate(x => x.Name, organisation);
            context.SaveChanges();
            #endregion
        }
    }
}
