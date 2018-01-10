using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels.Directory
{
    public class FileRemoveViewModel : DirectoryViewModel
    {
        public FileRemoveViewModel(string currentPath, string path) : base(currentPath, path)
        {
        }

        public override void RenderViewModel()
        {
            base.RenderViewModel();
            Console.WriteLine("Fileremoved seccessfully");
        }
    }
}
