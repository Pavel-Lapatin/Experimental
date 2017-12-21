using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.ViewModels
{
    public class CommandLineViewModel
    {
        public string CurrentPath { get; set; }

        public string QueryLineView => CurrentPath + "-->";


    }
}
