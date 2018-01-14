using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory;
using System;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        IMapper _mapper;
        private readonly FileManagerDbContext _context;
        private bool disposed;

        IRepositoryFactory _repositoryFactory;
      
        public UnitOfWork(FileManagerDbContext context,
            IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Commit()
        {
            _context.SaveChanges();
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

        public TEntity Get<TEntity>() where TEntity : class
                                                    
        {
            return _repositoryFactory.Get<TEntity>(new[] { _context });
        }

        public TEntity GetFileSystemManager<TEntity>() where TEntity : class
        {
            return _repositoryFactory.Get<TEntity>(null);
        }
    }
}