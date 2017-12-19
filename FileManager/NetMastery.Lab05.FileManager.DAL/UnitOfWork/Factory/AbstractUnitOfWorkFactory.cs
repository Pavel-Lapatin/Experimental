using NetMastery.Lab05.FileManager.DAL.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory
{
    public abstract class AbstractUnitOfWorkFactory
    {
        protected abstract IUnitOfWork Create<TItem>() where TItem : class;
        protected abstract IUnitOfWork Create<TItem1, TItem2>() where TItem1 : class
                                                             where TItem2 : class;
        protected abstract IUnitOfWork Create<TItem1, TItem2, TItem3> () where TItem1 : class
                                                                      where TItem2 : class
                                                                      where TItem3 : class;
    }
}
