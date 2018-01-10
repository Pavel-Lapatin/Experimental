using NetMastery.Lab05.FileManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account FindByLogin(string login);
        bool HasEnoughFreeSpace(string path, long fileSize);
    }
}
