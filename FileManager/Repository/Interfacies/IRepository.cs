using System.Collections.Generic;

namespace NetMastery.Lab05.FileManager.Repository
{
    public interface IRepository<T>
    {
        void AddItem(T item);
        IEnumerable<T> GetAll();

    }
}
