using NetMastery.Lab05.FileManager.UI.Interfaces;
using NetMastery.Lab05.FileManager.UI.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Implementation
{
    public class ResultProvider : IResultProvider
    {
        public ActionResult Result { get; set; }
    }
}
