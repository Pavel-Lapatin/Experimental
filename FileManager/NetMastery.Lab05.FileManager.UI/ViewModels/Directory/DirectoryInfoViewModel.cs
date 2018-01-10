using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.ViewModels.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class DirectoryInfoViewModel : DirectoryViewModel
    {
        public DirectoryStructureDto Directory { get; set; }

        public DirectoryInfoViewModel(string currentPath, string path) : base(currentPath, path)
        {
        }

        public override void RenderViewModel()
        {
            base.RenderViewModel();
            if(Directory != null)
            {
                Console.WriteLine();
                Console.WriteLine("Directory info: ");
                Console.WriteLine($"Directory name: {Directory.Name}");
                Console.WriteLine($"Creation date: {Directory.CreationDate.ToString("yy-MM-dd")}");
                Console.WriteLine($"Modification date: {Directory.ModificationDate.ToString("yy-MM-dd")}");
                Console.WriteLine($"DirectorySize: {Directory.DirectorySize}");
                Console.WriteLine($"FullPath: {Directory.FullPath}");
                Console.WriteLine();
            }
        }
    }

}
