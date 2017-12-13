using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.DAL.Entities;
using NetMastery.Lab05.FileManager.DAL.Interfacies;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FileManagerDbContext _context;
        public IAccountRepository Accounts { get; }
        public IStorageRepository Storagies { get; }

        public UnitOfWork(FileManagerDbContext context)
        {
            _context = context;
            Accounts = new AccountRepository(_context);
            Storagies = new StorageRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
