using NetMastery.Lab05.FileManager.Bl.Interfaces;
using System;
using System.Linq;
using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Domain;
using Serilog;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.DAL.Repository;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class AuthenticationService : IAuthenticationService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        #region Constructors

        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        #endregion

        public AccountDto Signin(string login, string password)
        {
            if (password == null || login == null)
            {
                Log.Logger.Debug($"AuthenticationService -- Signin -- input parameters are null");
                throw new ServiceArgumentNullException("Login and password must not be null or empty");
            }
            var account = _mapper.Map<AccountDto>(_unitOfWork
                .GetDbRepository<IDbAccountRepository>()
                .Find(x => x.Login == login)
                .FirstOrDefault());

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
                    throw new ServiceArgumentException("Password is wrong");
                }
            }
            else
            {
                Log.Logger.Information($"Unathorized acceess. Login doesn't exist");
                throw new ServiceArgumentNullException("Account with such login doesn't exist");
            }
        }

        public void Singup(string login, string password)
        {
            if (password == null || login == null)
            {
                Log.Logger.Debug($"AuthenticationService -- Signin -- input parameters are null");
                throw new ServiceArgumentException("Login and password must not be null or empty");
            }
            else
            {
                Log.Logger.Information($"Creating new account {login} ...");
                var newAccount = new Account
                {
                    AccountId = 0,
                    Login = login,
                    CreationDate = DateTime.Now,
                    Password = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt()),
                    RootDirectory = new DirectoryStructure
                    {
                        Name = login + "Root",
                        CreationDate = DateTime.Now,
                        ModificationDate = DateTime.Now,
                        FullPath = "~/" + login + "Root",

                    }
                };
                _unitOfWork.GetDbRepository<IDbAccountRepository>().Add(_mapper.Map<Account>(newAccount));
                _unitOfWork.Commit();
                Log.Logger.Information($"Created successfully");  
            }
        }
    }
}