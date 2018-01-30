using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NetMastery.InventoryManager.Bl.DtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.Servicies.Interfaces
{
    public interface IUserService : IBusinessService
    {
        Task<SignInStatus> PasswordSignInAsync(string login, string password, bool rememberMe);
        Task<IdentityResult> Register(UserDto user, string password);
        Task SignInAsync(UserDto user);
        Task<UserDto> FindByNameAsync(string userName);
        Task<bool> HasBeenVerifiedAsync();
        Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser);
        Task<IdentityResult> RegisterNewAccount(string email, string name, string phone, string password);
        Task<int> GetAccountIdAsync(string userId);
    }
}
