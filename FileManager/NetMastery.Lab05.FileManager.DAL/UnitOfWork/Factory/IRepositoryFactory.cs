using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory
{
    public interface IRepositoryFactory
    {
        TEntity GetRepository<TEntity>(object[] parameters) where TEntity : class;
    }
}
