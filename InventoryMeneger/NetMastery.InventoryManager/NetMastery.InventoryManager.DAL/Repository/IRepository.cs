using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity Find(int id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindByPredicaterAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindByPredicate(Expression<Func<TEntity, bool>> predicate);
    }
}
