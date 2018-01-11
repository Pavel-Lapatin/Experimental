using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Domain;
using System.Linq;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(FileManagerDbContext context) : base(context)
        {
        }

        public Account FindByLogin(string login)
        {
            return Find(x => x.Login == login).FirstOrDefault();
        }

        public Account FindByRootName(string rootName)
        {
            return Find(x => x.RootDirectory.Name == rootName).FirstOrDefault();
        }
    }
}
