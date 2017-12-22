using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.ViewModels
{
    public class AppViewModel
    {
        public string CurrentPath { get; set; }

        public string QueryLineView => CurrentPath + "-->";

        public string AuthenticatedLogin { get; set; }

        public AppViewModel()
        {
            CurrentPath = "~\\";
        }

    }
}
