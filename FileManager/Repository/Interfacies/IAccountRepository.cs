using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.BL;

namespace NetMastery.Lab05.FileManager.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        string GetPasswordByLogin(string login);
    }
}
