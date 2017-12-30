using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory
{
    public class DBRepositoryFactory : IDBRepositoryFactory
    {
        private Dictionary<string, object> dBRepositories;
        public DBRepositoryFactory()
        {

        }
        public IDBRepository<T> GetRepository<T>(FileManagerDbContext context) where T : class
        {
            if (dBRepositories == null)
            {
                dBRepositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!dBRepositories.ContainsKey(type))
            {
                var repositoryType = typeof(DBRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                dBRepositories.Add(type, repositoryInstance);
            }
            return (DBRepository<T>)dBRepositories[type];
        }
    }
}
