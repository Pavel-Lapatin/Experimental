using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IDBRepositoryFactory
    {
        IDBRepository<TEntity> GetRepository<TEntity>(FileManagerDbContext context) where TEntity : class;
    }
}
