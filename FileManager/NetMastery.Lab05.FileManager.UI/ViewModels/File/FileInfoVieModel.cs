using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.ViewModels.Directory;
using System;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class FileInfoVieModel : DirectoryViewModel
    {
        public FileStructureDto File { get; set; }

        public FileInfoVieModel(string currentPath, string path) : base(currentPath, path)
        {
        }

        public override void RenderViewModel()
        {
            if (File != null)
            {
                Console.WriteLine();
                Console.WriteLine("File info: ");
                Console.WriteLine($"File name: {File.Name}");
                Console.WriteLine($"Creation date: {File.CreationTime.ToString("yy-MM-dd")}");
                Console.WriteLine($"Modification date: {File.ModificationDate.ToString("yy-MM-dd")}");
                Console.WriteLine($"File Size: {File.FileSize}");
                Console.WriteLine($"Extenson: {File.Extension}");
                Console.WriteLine($"Download Numbers: {File.DownloadsNumber}");
                Console.WriteLine();
            }

        }
    }
}
