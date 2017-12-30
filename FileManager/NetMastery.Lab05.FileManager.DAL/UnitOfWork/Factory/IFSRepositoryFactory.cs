using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IFSRepositoryFactory
    {
        IFSRepository GetRepository<T>() where T : class;
    }
}
