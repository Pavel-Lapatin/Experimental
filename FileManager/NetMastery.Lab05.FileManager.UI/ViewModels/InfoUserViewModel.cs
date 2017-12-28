using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class InfoUserViewModel : ViewModel
    {
        public string Login { get; set; }
        public DateTime CreationDate { get; set; }
        public long MaxStorageSize { get; set; }
        public long CurentStorageSize { get; set; }
        public string RootDirectory { get; set; }

        public override void RenderViewModel()
        {
            Console.WriteLine();
            Console.WriteLine("User info: ");
            Console.WriteLine($"Login: {Login}");
            Console.WriteLine($"Creation date: {CreationDate.ToString("yy-MM-dd")}");
            Console.WriteLine($"Current Storage Size: {CurentStorageSize}");
            Console.WriteLine($"Max Storage Size: {MaxStorageSize }");
            Console.WriteLine($"Root directory: {RootDirectory}"); 
        }
    }
}
