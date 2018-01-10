using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.UI.Implementations
{
    public class ControllerFactory : IContrrollerFactory
    {
        IUserContext _userContext;
        public ControllerFactory(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public TEntity Get<TEntity>(object[] parameters) where TEntity : class
        {
            if (Repositories == null)
            {
                Repositories = new Dictionary<string, object>();
            }
            var type = typeof(TEntity);
            if (typeof(TEntity).IsAbstract || typeof(TEntity).IsInterface)
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

        Controller IContrrollerFactory.Get<TEntity>(object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
