using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class DbAccountRepository : DbRepository<Account>, IDbAccountRepository
    {
        public DbAccountRepository(FileManagerDbContext context) : base(context)
        {
        }

        public Account FindByLogin(string login)
        {
            return Find(x => x.Login == login).FirstOrDefault();
        }

        public bool IsFreeSpaceEnough(string path, long fileSize)
        {
            var rootFolderNAme = path.Trim().Split('\\')[1];
            var account = Find(x => x.RootDirectory.Name == rootFolderNAme).FirstOrDefault();
            if (account.MaxStorageSize - account.CurentStorageSize > fileSize)
            {
                account.CurentStorageSize += fileSize;
                return true;
            }
            return false;
        }
    }
}
