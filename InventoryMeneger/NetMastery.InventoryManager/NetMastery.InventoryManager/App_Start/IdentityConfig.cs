using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using NetMastery.InventoryManager.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using static NetMastery.InventoryManager.Models.InventoryManagerUser;

namespace NetMastery.InventoryManager
{


    public class InventoryManagerUserManager : UserManager<InventoryManagerUser>
    {
        public InventoryManagerUserManager(IUserStore<InventoryManagerUser> store)
            : base(store)
        {
        }

        public static InventoryManagerUserManager Create(IdentityFactoryOptions<InventoryManagerUserManager> options, IOwinContext context)
        {
            var manager = new InventoryManagerUserManager(new UserStore<InventoryManagerUser>(context.Get<InventoryManagerDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<InventoryManagerUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<InventoryManagerUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<InventoryManagerUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<InventoryManagerUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class InventoryManagerSignInManager : SignInManager<InventoryManagerUser, string>
    {
        public InventoryManagerSignInManager(InventoryManagerUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(InventoryManagerUser user)
        {
            return user.GenerateUserIdentityAsync((InventoryManagerUserManager)UserManager);
        }

        public static InventoryManagerSignInManager Create(IdentityFactoryOptions<InventoryManagerSignInManager> options, IOwinContext context)
        {
            return new InventoryManagerSignInManager(context.GetUserManager<InventoryManagerUserManager>(), context.Authentication);
        }
    } 
}