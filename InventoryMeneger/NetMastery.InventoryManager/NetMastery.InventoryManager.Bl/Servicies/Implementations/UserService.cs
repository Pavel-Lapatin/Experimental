using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Bl.Exceptions;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
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
            var User = await _unitOfWork.UserManager.FindUserByRoleAsync(accountId, roleName);
            return _mapper.Map<UserDto>(User);
        }
        public int GetAccountId(string UserId)
        {
            return _unitOfWork.UserManager.FindById(UserId).AccountId;
        }
        public SignInStatus PasswordSignIn(string UserName, string password, bool rememberMe)

        {
            return  _unitOfWork.SignInManager.PasswordSignIn(UserName, password, rememberMe, false);
        }
        public async Task<IdentityResult> Register(UserDto user, string password)
        {
            return await _unitOfWork.UserManager.CreateAsync(_mapper.Map<User>(user), password);
        }
        public async Task SignInAsync(UserDto User)
        {
             await _unitOfWork.SignInManager.SignInAsync(_mapper.Map<User>(User), false, false);
        }
        public UserDto FindByName(string UserName)
        {
            var User = _unitOfWork.UserManager.FindByName(UserName);
            return _mapper.Map<UserDto>(User);
        }
        public async Task<bool> HasBeenVerifiedAsync()
        {
            return await _unitOfWork.SignInManager.HasBeenVerifiedAsync();
        }
        public async Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            return await _unitOfWork.SignInManager.TwoFactorSignInAsync(provider, code, isPersistent, rememberBrowser);
        }
        public async Task<IdentityResult> AddRoleAsync(string UserId, string role)
        {
            return await _unitOfWork.UserManager.AddToRoleAsync(UserId, role);
        }
        public async Task<IdentityResult> RegisterNewAccount(string name, string email, string phone, string password)
        {
            var User = new User { UserName = name, Email = email, PhoneNumber = phone, Account = new Account() };
            IdentityResult result = null;
            try
            {
               result = await _unitOfWork.UserManager.CreateAsync(User, password);
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
               result =  await _unitOfWork.UserManager.AddToRoleAsync(User.Id, "admin");
                await _unitOfWork.SignInManager.SignInAsync(User, false, false);
            }
            return result;
        }
        public IEnumerable<UserDto> GetAll(int accountId)
        {
            var users =  _unitOfWork.UserManager.GetAllForAccountId(accountId).Select(item => _mapper.Map<UserDto>(item));
            var roles = _unitOfWork.RoleManager.GetAll();
            foreach (var user in users)
            {
                user.RoleName = _unitOfWork.RoleManager.GetUserRoleNameById(user.Id);
            }
            return users;
        }
        public IdentityResult Add(UserDto user, string password)
        {
            try
            {
                return  _unitOfWork.UserManager.Create(_mapper.Map<User>(user), password);
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public IdentityResult Delete(UserDto user)
        {
            try
            {
                return _unitOfWork.UserManager.Delete(_mapper.Map<User>(user));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public IdentityResult Update(UserDto user)
        {
            try
            {
                return _unitOfWork.UserManager.Update(_mapper.Map<User>(user));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public IEnumerable<UserDto> SearchByPattern(int accountId, string name, string email, string phone, string role)
        {
            var users = _unitOfWork.UserManager.SearchByPattern(accountId, name, email, phone, role).Select(item => _mapper.Map<UserDto>(item)).ToArray();
            var roles = _unitOfWork.RoleManager.GetAll();
            foreach (var user in users)
            {
                user.RoleName = _unitOfWork.RoleManager.GetUserRoleNameById(user.Id);
            }
            return users;
        }
    }
}
