using NetMastery.Lab05.FileManager.DAL.Entities;
using System.Collections;
using System.Collections.Generic;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IDirectoryRepository : IRepository<DirectoryStructure>
    {
        DirectoryStructure GetAllEagerLoading(int directoryId);
    }
}
