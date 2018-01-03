using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        protected Dictionary<string, object> Repositories;

        public TEntity GetRepository<TEntity>(object[] parameters) where TEntity : class
        {
            if (Repositories == null)
            {
               Repositories = new Dictionary<string, object>();
            }
            var type = typeof(TEntity);
            if(typeof(TEntity).IsAbstract || typeof(TEntity).IsInterface)
            {
                type = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(x => typeof(TEntity).IsAssignableFrom(x) && x.IsClass);
            }

            if (!Repositories.ContainsKey(type.Name))
            {
                var repositoryInstance = Activator.CreateInstance(type, parameters);
                Repositories.Add(type.Name, repositoryInstance);
            }
            return (TEntity)Repositories[type.Name];
        }
    }
}
