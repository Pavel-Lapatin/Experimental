using NetMastery.FileManager.Bl.Interfaces;
using System;
using System.Linq;
using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Domain;
using Serilog;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class AuthenticationService : IAuthenticationService
    {
        IUnitOfWork _unitOfWork;

        #region Constructors

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #endregion

        public UserInfo Signin(string login, string password)
        {
            if (password == null || login == null)
            {
                throw new NullReferenceException("Login and password must not be null or empty");
            }
            var account = Mapper.Instance.Map<AccountDto>(_unitOfWork.Repository<Account>().Find(x => x.Login == login).FirstOrDefault());
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    Log.Logger.Information($"{login} registered in the system");
                    return account;
                }
                else
                {
                    Log.Logger.Information($"Unathorized acceess: login {login}");
                    throw new ArgumentException("Password is wrong");
                }
            }
            else
            {
                Log.Logger.Information($"Unathorized acceess. Login doesn't exist");
                throw new NullReferenceException("Account with such login doesn't exist");
            }
        }

        public void Singup(string login, string password)
        {
            if (password == null || login == null)
            {
                Log.Logger.Information($"Empty input used, authentication controller");
                throw new NullReferenceException("Login and password must not be null or empty");
            }
            else
            {
                var newAccount = new AccountDto
                {
                    AccountId = 0,
                    Login = login,
                    CreationDate = DateTime.Now,
                    Password = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt()),
                    RootDirectory = new DirectoryStructureDto
                    {
                        Name = login + "Root",
                        CreationDate = DateTime.Now,
                        ModificationDate = DateTime.Now,
                        FullPath = "~/" + login + "Root",

                    }
                };
                _unitOfWork.Repository<Account>().Add(Mapper.Instance.Map<Account>(newAccount));
                _unitOfWork.Commit();
                Log.Logger.Information($"Create new account: {login}");
            }
        }
    }
}