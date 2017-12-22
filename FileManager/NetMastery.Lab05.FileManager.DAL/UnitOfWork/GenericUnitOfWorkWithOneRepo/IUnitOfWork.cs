using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        void Commit();
    }
}

