using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IDbRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> EagerFind(Expression<Func<TEntity, bool>> predicate, 
            Expression<Func<TEntity, ICollection<TEntity>>> predicate2);

        IEnumerable<TEntity> EagerFind<TCollectionEntity>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, ICollection<TCollectionEntity>>> predicate2)
            where TCollectionEntity : class;

        IEnumerable<TEntity> EagerFind<TCollectionEntity, TCollectionEntity2>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, ICollection<TCollectionEntity>>> predicate2,
            Expression<Func<TEntity, ICollection<TCollectionEntity2>>> predicate3)
            where TCollectionEntity : class
            where TCollectionEntity2 : class;

        IEnumerable<TEntity> EagerFind(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> predicate2);


        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
