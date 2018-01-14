using NetMastery.Lab05.FileManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Views
{
    public class FileInfoView : View
    {
        private readonly FileStructureDto _file;

        public FileInfoView(FileStructureDto file)
        {
            _file = file;
        }
        public override void RenderView()
        {
            if (_file != null)
            {
                Console.WriteLine();
                Console.WriteLine("File info: ");
                Console.WriteLine($"File name: {_file.Name}");
                Console.WriteLine($"Creation date: {_file.CreationTime.ToString("yy-MM-dd")}");
                Console.WriteLine($"Modification date: {_file.ModificationDate.ToString("yy-MM-dd")}");
                Console.WriteLine($"File Size: {_file.FileSize}");
                Console.WriteLine($"Extenson: {_file.Extension}");
                Console.WriteLine($"Download Numbers: {_file.DownloadsNumber}");
                Console.WriteLine();
            }
        }
    }
}
