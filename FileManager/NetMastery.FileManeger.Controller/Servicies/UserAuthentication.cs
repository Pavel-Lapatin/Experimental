using NetMastery.FileManeger.Bl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.BLModel.DtoClasses;

namespace NetMastery.FileManeger.Bl.Servicies
{
    public class UserAuthentication : IUserAuthentication
    {
        public UserInfo Signin(string login, string password)
        {
            if (password == null || login == null)
            {
                throw new NullReferenceException("Login and password must not be null or empty");
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

        public UserInfo Singup(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
