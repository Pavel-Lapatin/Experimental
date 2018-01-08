using NetMastery.Lab05.FileManager.DAL.Repository;
using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IUnitOfWork : IDisposable 
    {
        TEntity GetDbRepository<TEntity>() where TEntity : class;
        TEntity GetFileSystemManager<TEntity>() where TEntity : class;
        void Commit();
    }
}

