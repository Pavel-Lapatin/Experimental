using System;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class FileInfoVieModel : ViewModel
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public long FileSize { get; set; }
        public int DownloadsNumber { get; set; }
        public string Extension { get; set; }
        public string DirectoryPath { get; set; }

        public override void RenderViewModel()
        {
            Console.WriteLine();
            Console.WriteLine("File info: ");
            Console.WriteLine($"File name: {Name}");
            Console.WriteLine($"Creation date: {CreationDate.ToString("yy-MM-dd")}");
            Console.WriteLine($"Modification date: {ModificationDate.ToString("yy-MM-dd")}");
            Console.WriteLine($"File Size: {FileSize}");
            Console.WriteLine($"Extenson: {Extension}");
            Console.WriteLine($"Download Numbers: {DownloadsNumber}");
            Console.WriteLine();

        }
    }
}
