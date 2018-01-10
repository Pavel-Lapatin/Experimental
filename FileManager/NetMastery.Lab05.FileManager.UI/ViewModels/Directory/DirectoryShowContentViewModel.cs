using NetMastery.Lab05.FileManager.UI.ViewModels.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class DirectoryShowContentViewModel : DirectoryViewModel
    {
        public IDictionary<string, IList<string>> Data { get; set; }

        public DirectoryShowContentViewModel(string currentPath, string path) : base(currentPath, path)
        {   
        }
        public override void RenderViewModel()
        {
            base.RenderViewModel();
            if (Data != null)
            {
                foreach (var key in Data.Keys)
                {
                    Console.WriteLine($"{key}:");
                    foreach (var item in Data[key])
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine();
                };
            }
            
        }
    }
}
