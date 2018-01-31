using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.Servicies.Interfaces
{
    public interface IBusinessService<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
        void DeleteRange(IEnumerable<TEntity> item);
    }
}
