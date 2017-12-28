using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using System.Linq.Expressions;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class DBRepository<TEntity> : IDBRepository<TEntity> where TEntity : class
    {
        protected readonly FileManagerDbContext Context;

        public DBRepository(FileManagerDbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> EagerFind(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, ICollection<TEntity>>> predicate2)
        {
            return Context.Set<TEntity>().Where(predicate).Include(predicate2).ToList();
        }
        public IEnumerable<TEntity> EagerFind<TCollectionEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, ICollection<TCollectionEntity>>> predicate2) where TCollectionEntity : class
        {
            return Context.Set<TEntity>().Where(predicate).Include(predicate2).ToList();
        }
        public IEnumerable<TEntity> EagerFind<TCollectionEntity, TCollectionEntity2>(Expression<Func<TEntity, bool>> predicate, 
            Expression<Func<TEntity, ICollection<TCollectionEntity>>> predicate2,
            Expression<Func<TEntity, ICollection<TCollectionEntity2>>> predicate3
            ) where TCollectionEntity : class
              where TCollectionEntity2 : class
        {
            return Context.Set<TEntity>().Where(predicate).Include(predicate2).Include(predicate3).ToList();
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
