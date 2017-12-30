using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IUnitOfWork : IDisposable
    {
        IDBRepository<T> DBRepository<T>() where T : class;
        IFSRepository FSRepository<FSEntity>() where FSEntity : class;
        void Commit();
    }
}

