using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        //Task CreateAsync(TEntity item);
        void Remove(TEntity item);
        //Task RemoveAsync(TEntity item);
        void RemoveRange(IEnumerable<TEntity> items);
        //Task RemoveRangeAsync(IEnumerable<TEntity> item);
        void Update(TEntity item);
        //Task UpdateAsync(TEntity item);
        TEntity FindById(params object[] key);
        Task<TEntity> FindByIdAsync(params object[] key);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> FindByPredicate(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindByPredicateAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
