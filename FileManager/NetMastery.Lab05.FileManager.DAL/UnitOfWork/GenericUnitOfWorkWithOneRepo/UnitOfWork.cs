using NetMastery.Lab05.FileManager.DAL.Interfacies;
using System.Collections.Generic;
using System.Data.Entity;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    
    public class UnitOfWork<TEntity> : IUnitOfWork
    {
        private readonly IEqualityComparer<T> comparer;
        private readonly HashSet<T> inserted;
        private readonly HashSet<T> updated;
        private readonly HashSet<T> deleted;
        protected UnitOfWork()
        protected UnitOfWork(IEqualityComparer<T> comparer)
    protected IEqualityComparer<T> Comparer { get; }
        protected virtual ICollection<T> InsertedItems { get; }
        protected virtual ICollection<T> UpdatedItems { get; }
        protected virtual ICollection<T> DeletedItems { get; }
        public virtual bool HasPendingChanges { get; }
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    protected virtual void AcceptChanges()
    protected abstract bool IsNew(T item)
    public virtual void RegisterNew(T item)
    public virtual void RegisterChanged(T item)
    public virtual void RegisterRemoved(T item)
    public virtual void Unregister(T item)
    public virtual void Rollback()
    public abstract Task CommitAsync(CancellationToken cancellationToken);
    }
}
