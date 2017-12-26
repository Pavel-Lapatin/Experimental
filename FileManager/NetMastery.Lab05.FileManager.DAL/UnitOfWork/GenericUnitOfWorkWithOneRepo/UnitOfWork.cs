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
        private Dictionary<string, object> repositories;

        public UnitOfWork(FileManagerDbContext context)
        {
            _context = context;
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

        public IDBRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(DBRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }
            return (DBRepository<T>)repositories[type];
        }
    }
}