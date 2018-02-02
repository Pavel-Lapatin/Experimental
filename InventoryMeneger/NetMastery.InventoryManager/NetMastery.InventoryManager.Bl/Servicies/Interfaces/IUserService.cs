using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NetMastery.InventoryManager.Bl.DtoEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.Servicies.Interfaces
{
    public interface IUserService : IDisposable
    {
        SignInStatus PasswordSignIn(string login, string password, bool rememberMe);

        Task<IdentityResult> Register(UserDto user, string password);
        Task SignInAsync(UserDto user);
        UserDto FindByName(string userName);
        Task<bool> HasBeenVerifiedAsync();
        Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser);
        Task<IdentityResult> RegisterNewAccount(string email, string name, string phone, string password);
        int GetAccountId(string userId);
        Task<UserDto> FindUserByRole(int accountId, string roleName);
        IEnumerable<UserDto> GetAll(int accountId);
        IdentityResult Update(UserDto user);
        IdentityResult Delete(UserDto user);
        IEnumerable<UserDto> SearchByPattern(int accountId, string Name, string Email, string Phone, string Role);
    }
}
