using NetMastery.Lab05.FileManager.DAL.Interfacies;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly FileManagerDbContext _context;
        private bool disposed;

        IFSRepositoryFactory _fSRepositoryFactory;
        IDBRepositoryFactory _dBRepocitoryFactory;
      
        public UnitOfWork(FileManagerDbContext context,
            IFSRepositoryFactory fSRepositoryFactory,
            IDBRepositoryFactory dBRepocitoryFactory)
        {
            _fSRepositoryFactory = fSRepositoryFactory;
            _dBRepocitoryFactory = dBRepocitoryFactory;
            _context = context;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {

            } 
        }

        public IDBRepository<T> DBRepository<T>() where T : class
        {
            return _dBRepocitoryFactory.GetRepository<T>(_context);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IFSRepository FSRepository<FSEntity>() where FSEntity : class
        {
            return _fSRepositoryFactory.GetRepository<FSEntity>();
        }
    }
}