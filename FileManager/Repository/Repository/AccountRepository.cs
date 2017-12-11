using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NetMastery.Lab05.FileManager.BL;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Entities;

namespace NetMastery.Lab05.FileManager.Repository.Repository
{
    
    public class AccountRepository : IAccountRepository
    {
        private readonly FileManagerDb _context = new FileManagerDb("FileMAneger");
        private IMapper _mapper;

        public void AddItem(BL.AccountBl item)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Account, AccountBl>();
            });
            _mapper = configuration.CreateMapper();
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
