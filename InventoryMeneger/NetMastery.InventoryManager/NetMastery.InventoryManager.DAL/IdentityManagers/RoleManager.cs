using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.DAL.IdentityManagers
{
    public class InventoryRoleManager : RoleManager<IdentityRole>
    {
        public InventoryRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }

        public static InventoryRoleManager Create(IdentityFactoryOptions<InventoryRoleManager> options, IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<InventoryDbContext>());
            return new InventoryRoleManager(roleStore);
        }
        public IEnumerable<IdentityRole> GetAll()
        {
            using (var context = new InventoryDbContext())
            {
                return context.Roles.ToArray();
            }

        }
        public string GetUserRoleNameById(string userId)
        {
            using (var context = new InventoryDbContext())
            {
                var roleId = context.Users.Find(userId).Roles.FirstOrDefault().RoleId;
                return context.Roles.Find(roleId).Name;
            }

        }
    }
}
