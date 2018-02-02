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

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void RemoveRange(IEnumerable<TEntity> items)
        {
            foreach(var item in items)
            {
                _dbSet.Attach(item);
                _dbSet.Remove(item);
            }
        }

        public TEntity Find(int id)
        {
            return _dbSet.Find(id);
        }
        public async Task<TEntity> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IEnumerable<TEntity> FindByPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
        public async Task<IEnumerable<TEntity>> FindByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
             return await _dbSet.AsNoTracking().ToListAsync();    
        }

        public TEntity FindById(params object[] key)
        {
            return _dbSet.Find(key);
        }

        public async Task<TEntity> FindByIdAsync(params object[] key)
        {
            return await _dbSet.FindAsync(key);
        }
    }
}
