using System;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts {get;}
        IDirectoryRepository Storagies { get; }
        int Complete();
    }
}
