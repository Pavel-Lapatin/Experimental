using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using NetMastery.InventoryManager.Domain;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.Servicies.Implementations
{
    public class UserService : BusinessService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public async Task<UserDto> FindUserByRole(int accountId, string roleName)
        {
            var user = await _unitOfWork.UserManager.FindUserByRoleAsync(accountId, roleName);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<int> GetAccountIdAsync(string userId)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            return user.AccountId;
        }

        public async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe)
        {
            return await _unitOfWork.SignInManager.PasswordSignInAsync(userName, password, rememberMe, false);
        }
        public async Task<IdentityResult> Register(UserDto user, string password)
        {
            return await _unitOfWork.UserManager.CreateAsync(_mapper.Map<User>(user), password);
        }
        public async Task SignInAsync(UserDto user)
        {
             await _unitOfWork.SignInManager.SignInAsync(_mapper.Map<User>(user), false, false);
        }
        public async Task<UserDto> FindByNameAsync(string userName)
        {
            return _mapper.Map<UserDto>(await _unitOfWork.UserManager.FindByNameAsync(userName));
        }
        public async Task<bool> HasBeenVerifiedAsync()
        {
            return await _unitOfWork.SignInManager.HasBeenVerifiedAsync();
        }

        public async Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            return await _unitOfWork.SignInManager.TwoFactorSignInAsync(provider, code, isPersistent, rememberBrowser);
        }

        public async Task<IdentityResult> AddRoleAsync(string userId, string role)
        {
            return await _unitOfWork.UserManager.AddToRoleAsync(userId, role);
        }

        public async Task<IdentityResult> RegisterNewAccount(string name, string email, string phone, string password)
        {
            var user = new User { UserName = name, Email = email, PhoneNumber = phone, Account = new Account() };
            IdentityResult result = null;
            try
            {
               result = await _unitOfWork.UserManager.CreateAsync(user, password);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            if (result.Succeeded)
            {
               result =  await _unitOfWork.UserManager.AddToRoleAsync(user.Id, "admin");
                await _unitOfWork.SignInManager.SignInAsync(user, false, false);
            }
            return result;
        }
    }
}
