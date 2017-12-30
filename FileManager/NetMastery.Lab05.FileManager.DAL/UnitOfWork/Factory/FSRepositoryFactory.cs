using NetMastery.Lab05.FileManager.DAL.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory
{
    public class FSRepositoryFactory : IFSRepositoryFactory
    {
        private Dictionary<string, object> fSRepositories;

        public FSRepositoryFactory()
        {

        }
        public IFSRepository GetRepository<T>() where T : class
        {
            if (fSRepositories == null)
            {
                fSRepositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!fSRepositories.ContainsKey(type))
            {
                var repositoryType = typeof(IFSRepository);
                var repositoryInstance = Activator.CreateInstance(typeof(T), null);
                fSRepositories.Add(type, repositoryInstance);
            }
            return (IFSRepository)fSRepositories[type];
        }
    }
}
