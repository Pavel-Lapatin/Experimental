
using NetMastery.Lab05.FileManager.DAL.Interfacies;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class StorageRepository : Repository<Storage>, IStorageRepository
    {
        public StorageRepository(FileManagerDBContext context) : base(context)
        {
        }

    }
}
