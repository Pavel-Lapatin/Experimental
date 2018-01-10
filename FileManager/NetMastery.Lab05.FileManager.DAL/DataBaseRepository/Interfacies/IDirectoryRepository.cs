using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public interface IDirectoryRepository : IRepository<DirectoryStructure>
    {

        DirectoryStructure FindByPath(string path);
        DirectoryStructure FindByPathEagerLoadingParentDirectory(string path);
        DirectoryStructure FindByPathEagerLoadingFiles(string path);
        DirectoryStructure FindByPathEagerLoadingChildrenDirectories(string path);
        DirectoryStructure FindByPathEagerLoadingFull(string path);
        IEnumerable<DirectoryStructure> FindDirectoriesWhichContainPath(string path);
    }
}
