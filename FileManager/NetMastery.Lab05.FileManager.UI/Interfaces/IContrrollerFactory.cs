using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Interfaces
{
    public interface IContrrollerFactory
    {
        Controller Get<TEntity>(object[] parameters) where TEntity : Controller;
    }
}
