using NetMastery.Lab05.FileManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Views
{
    public class DirectoryInfoView : View
    {
        private readonly DirectoryStructureDto _directory;
        public DirectoryInfoView(DirectoryStructureDto directory)
        {
            _directory = directory;
        }
        public override void RenderView()
        {
            if (_directory != null)
            {
                Console.WriteLine();
                Console.WriteLine("Directory info: ");
                Console.WriteLine($"Directory name: {_directory.Name}");
                Console.WriteLine($"Creation date: {_directory.CreationDate.ToString("yy-MM-dd")}");
                Console.WriteLine($"Modification date: {_directory.ModificationDate.ToString("yy-MM-dd")}");
                Console.WriteLine($"DirectorySize: {_directory.DirectorySize / 1024} kB");
                Console.WriteLine($"FullPath: {_directory.FullPath}");
                Console.WriteLine();
            }
        }
    }
}
