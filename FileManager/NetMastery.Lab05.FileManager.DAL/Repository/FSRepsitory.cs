﻿//using NetMastery.Lab05.FileManager.DAL.Interfacies;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NetMastery.Lab05.FileManager.DAL.Repository
//{
//    class FSRepsitory<TEntity> : IFSRepository<TEntity> where TEntity : class
//    {
//        public void Add(TEntity entity)
//        {
//            Context.Set<TEntity>().Add(entity);
//        }

//        public void AddRange(IEnumerable<TEntity> entities)
//        {
//            Context.Set<TEntity>().AddRange(entities);
//        }

//        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
//        {
//            return Context.Set<TEntity>().Where(predicate);
//        }

//        public IEnumerable<TEntity> EagerFind(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, ICollection<TEntity>>> predicate2)
//        {
//            return Context.Set<TEntity>().Where(predicate).Include(predicate2).ToList();
//        }
//        public IEnumerable<TEntity> EagerFind<TCollectionEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, ICollection<TCollectionEntity>>> predicate2) where TCollectionEntity : class
//        {
//            return Context.Set<TEntity>().Where(predicate).Include(predicate2).ToList();
//        }

//        public TEntity Get(int id)
//        {
//            return Context.Set<TEntity>().Find(id);
//        }

//        public IEnumerable<TEntity> GetAll()
//        {
//            return Context.Set<TEntity>().ToList();
//        }

//        public void Remove(TEntity entity)
//        {
//            Context.Set<TEntity>().Remove(entity);
//        }

//        public void RemoveRange(IEnumerable<TEntity> entities)
//        {
//            Context.Set<TEntity>().RemoveRange(entities);
//        }
//    }
//}
