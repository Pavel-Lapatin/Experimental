using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IUnitOfWork<T> : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        bool HasPendingChanges { get; }
        RegisterNew(T item) ;
        void RegisterChanged(T item);
        void RegisterRemoved(T item);
        void Unregister(T item);
        void Rollback();
        Task CommitAsync(CancellationToken cancellationToken);
    }
}

