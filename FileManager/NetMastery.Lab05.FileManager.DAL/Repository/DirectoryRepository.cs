using NetMastery.Lab05.FileManager.DAL.Entities;
using NetMastery.Lab05.FileManager.DAL.Interfacies;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class DirectoryRepository : Repository<DirectoryStructure>, IDirectoryRepository
    {
        public DirectoryRepository(FileManagerDbContext context) : base(context)
        {
        }

    }
}
