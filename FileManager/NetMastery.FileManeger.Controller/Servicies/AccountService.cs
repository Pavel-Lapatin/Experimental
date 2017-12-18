using System;
using System.Linq;
using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Entities;
using NetMastery.Lab05.FileManager.DAL.Repository;

using System.Resources;
using NetMastery.Lab05.FileManager.BL.Dto;

namespace NetMastery.Lab05.FileManager.BL.Servicies
{
    public class AccountService : IDisposable
    {
        private ResourceManager rm;
        private UnitOfWork unitOfWork;

        public AccountService(ResourceManager rm, UnitOfWork unitOfWork)
        {
            this.rm = rm;
            this.unitOfWork = unitOfWork;
        }

        #region Autentication

        public AccountDto GetUserInfo(int accountId)
        {
            return Mapper.Instance.Map<AccountDto>(unitOfWork.Accounts.Get(accountId));
        }


        public bool VerifyPassword(string login, string password, FileManagerModel model)
        {

            if (password == null || login == null)
            {
                throw new NullReferenceException(rm.GetString("LoginNullReferenceException"));
            }
            var account = Mapper.Map<AccountDto>(unitOfWork.Accounts.Find(x => x.Login == login).FirstOrDefault());
            if (BCrypt.Net.BCrypt.Verify(password, account.Password))
            {
                model.LoginName = account.Login;
                model.AccountId = account.AccountId;
                model.RootDirectoryId = account.RootDirectory.DirectoryId;
                return true;
            }
            return false;

        }

        #endregion

        #region Registration

        public void RegisterUser(string login, string password)
        {
            if (password == null || login == null)
            {
                throw new NullReferenceException(rm.GetString("LoginNullReferenceException"));
            }
            var newAccount = new AccountDto
            {
                AccountId = 0,
                Login = login,
                CreationDate = DateTime.Now,
                Password = BCrypt.Net.BCrypt.HashPassword(password,
                                                          BCrypt.Net.BCrypt.GenerateSalt()),
                RootDirectory = new DirectoryStructureDto
                {
                    Name = login + "Root",
                    CreationDate = DateTime.Now,
                    ModificationDate = DateTime.Now,
                    FullPath = "CommonStorage/" + login + "Root",

                }
            };
            unitOfWork.Accounts.Add(Mapper.Instance.Map<Account>(newAccount));
            unitOfWork.Complete();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }


        #endregion
    }
}
