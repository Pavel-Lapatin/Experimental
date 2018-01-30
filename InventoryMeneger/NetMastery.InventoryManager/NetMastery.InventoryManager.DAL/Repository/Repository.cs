using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public void Create(TEntity item)
        {
            _dbSet.Add(item);
        }

        public TEntity Find(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<IEnumerable<TEntity>> FindByPredicaterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public IEnumerable<TEntity> FindByPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            var x = _dbSet.Where(predicate).ToList();
            return  _dbSet.Where(predicate).ToList();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
             return await _dbSet.AsNoTracking().ToListAsync();    
        }


        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
