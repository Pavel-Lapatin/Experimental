using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using NetMastery.InventoryManager.DAL.IdentityManager.Extension;
using NetMastery.InventoryManager.Domain;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.DAL.IdentityManagers
{
    public class InventorySignInManager : SignInManager<User, string>
    {
        public InventorySignInManager(InventoryUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((InventoryUserManager)UserManager);
        }

        public static InventorySignInManager Create(IdentityFactoryOptions<InventorySignInManager> options, IOwinContext context)
        {
            var x = new InventorySignInManager(context.GetUserManager<InventoryUserManager>(), context.Authentication);
            return x;
        }
    }
}
