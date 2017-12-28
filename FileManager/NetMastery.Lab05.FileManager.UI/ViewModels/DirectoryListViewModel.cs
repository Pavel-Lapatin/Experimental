using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class DirectoryListViewModel : StringListViewModel
    {
        public string Path { get; set; }

        public DirectoryListViewModel(IList<string> data, string path) : base(data)
        {
            Path = path;
        }
        public override void RenderViewModel()
        {  
            if(Data.Count == 0)
            {
                Console.WriteLine($"There are following catalogs and files in {Path}: ");
                base.RenderViewModel();
            }
            else
            {
                Console.WriteLine($"There directory {Path} is empty");
            }        
        }
    }
}
