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
    }
}
