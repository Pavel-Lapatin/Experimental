using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class DirectorySearchVIewModel : StringListViewModel
    {
        public DirectorySearchVIewModel(IList<string> data) : base(data)
        {
        }
        public override void RenderViewModel()
        {
            Console.WriteLine("Search result: ");
            base.RenderViewModel();
        }
    }
}
