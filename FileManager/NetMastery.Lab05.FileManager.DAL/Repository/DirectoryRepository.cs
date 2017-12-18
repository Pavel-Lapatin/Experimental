using System.Collections.Generic;
using NetMastery.Lab05.FileManager.DAL.Entities;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using System.Linq;
using System.Data.Entity;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class DirectoryRepository : Repository<DirectoryStructure>, IDirectoryRepository
    {
        public DirectoryRepository(FileManagerDbContext context) : base(context)
        {

        }

        DirectoryStructure IDirectoryRepository.GetAllEagerLoading(int directoryId)
        {
            return ((FileManagerDbContext)Context).Directories.Find(directoryId);
             
        }

        
    }
}
