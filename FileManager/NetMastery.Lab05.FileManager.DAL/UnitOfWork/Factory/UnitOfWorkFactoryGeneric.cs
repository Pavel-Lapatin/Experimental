using NetMastery.Lab05.FileManager.DAL.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory
{
    public interface IUnitOfWorkFaktory<TItem> where TItem : IUnitOfWork
    {
        T CreateUnitOfWork<T>();
        
    }

}