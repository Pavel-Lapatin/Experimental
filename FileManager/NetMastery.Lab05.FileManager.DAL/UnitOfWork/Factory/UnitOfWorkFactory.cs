using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.DAL.Interfacies;

namespace NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory
{
    public class UnitOfWorkFactory : AbstractUnitOfWorkFactory
    {
        public override IUnitOfWork Create<TItem>()
        {
            throw new NotImplementedException();
        }

        public override IUnitOfWork Create<TItem1, TItem2>()
        {
            throw new NotImplementedException();
        }

        public override IUnitOfWork Create<TItem1, TItem2, TItem3>()
        {
            throw new NotImplementedException();
        }
    }
}
