using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Models
{
    public class InventoryManagerUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<InventoryManagerUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
    public class InventoryManagerDbContext : IdentityDbContext<InventoryManagerUser>
    {
        public InventoryManagerDbContext()
            : base("InventoryManagerConnection", throwIfV1Schema: false)
        {
        }

        public static InventoryManagerDbContext Create()
        {
            return new InventoryManagerDbContext();
        }
    }
    
}