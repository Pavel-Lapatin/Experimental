using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.BL;
using NetMastery.Lab05.FileManager.DAL;

namespace NetMastery.Lab05.FileManager.Repository.Repository
{
    
    public class AccountRepository : IAccountRepository
    {
        private FileManagerDb context = new FileManagerDb("FileMAneger");

        public void AddItem(Account item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public string GetPasswordByLogin(string login)
        {
            return context.Accounts.FirstOrDefault(x => x.Login == login).Password;

        }
    }
}
