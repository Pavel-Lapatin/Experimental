using NetMastery.Lab05.FileManager.DAL.Exceptions;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly FileManagerDbContext _context;
        private bool disposed;

        IRepositoryFactory _repositoryFactory;
      
        public UnitOfWork(FileManagerDbContext context,
            IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _context = context;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new DbRepositoryArgumentException("Data base operations failed");
            } 
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

       
        public TEntity GetDbRepository<TEntity>() where TEntity : class
                                                    
        {
            return _repositoryFactory.GetRepository<TEntity>(new[] { _context });
        }

        public TEntity GetFileSystemManager<TEntity>() where TEntity : class
        {
            return _repositoryFactory.GetRepository<TEntity>(null);
        }
    }
}