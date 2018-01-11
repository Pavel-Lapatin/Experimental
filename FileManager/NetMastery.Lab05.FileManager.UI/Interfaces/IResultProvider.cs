using NetMastery.Lab05.FileManager.UI.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Interfaces
{
    public interface IResultProvider
    {
        ActionResult Result { get; set; }
    }
}
