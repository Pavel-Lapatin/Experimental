using System;
using System.Collections.Generic;
using System.Linq;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Entities;

namespace NetMastery.Lab05.FileManager.Repository.Repository
{
    
    public class AccountRepository : IAccountRepository
    {
        private readonly FileManagerDBContext _context = new FileManagerDBContext();
        
        public void AddItem(BL.AccountBl item)
        {

        }

        public IEnumerable<AccountBl> GetAll()
        {
            throw new NotImplementedException();
        }

        public void ConfigureAutoMapper()
        {
            
        }

        public string GetPasswordByLogin(string login)
        {
            return _context.Accounts.FirstOrDefault(x => x.Login == login)?.Password;
        }

        public AccountBl GetAccountByLogin(string login)
        {
            return _mapper.Map<AccountBl>(_context.Accounts.FirstOrDefault(x => x.Login == login));
        }
    }
}
