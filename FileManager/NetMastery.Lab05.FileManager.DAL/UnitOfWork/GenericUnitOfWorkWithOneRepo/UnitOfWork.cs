using NetMastery.Lab05.FileManager.DAL.Interfacies;
using System.Collections.Generic;
using System.Data.Entity;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        private FileManagerDbContext _context;

        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}
