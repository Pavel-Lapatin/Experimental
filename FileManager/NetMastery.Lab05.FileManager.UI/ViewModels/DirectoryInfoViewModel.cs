using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class DirectoryInfoViewModel : ViewModel
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public long DirectorySize { get; set; }
        public string FullPath { get; set; }

        public override void RenderViewModel()
        {
            Console.WriteLine();
            Console.WriteLine("Directory info: ");
            Console.WriteLine($"Directory name: {Name}");
            Console.WriteLine($"Creation date: {CreationDate.ToString("yy-MM-dd")}");
            Console.WriteLine($"Modification date: {ModificationDate.ToString("yy-MM-dd")}");
            Console.WriteLine($"DirectorySize: {DirectorySize}");
            Console.WriteLine($"FullPath: {FullPath}");
        }
    }

}
