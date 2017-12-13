

using System;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts {get;}
        IStorageRepository Storagies { get; }
        int Complete();
    }
}
