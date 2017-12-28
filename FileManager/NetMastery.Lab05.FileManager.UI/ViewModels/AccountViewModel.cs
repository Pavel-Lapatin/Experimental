using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class AccountViewModel : ViewModel
    {
        public string Login { get; set; }
        public string RootDirectory { get; set; }

        public override void RenderViewModel()
        {
            Console.WriteLine();
            Console.WriteLine("Account info: ");
            Console.WriteLine($"Login: {Login}");
            Console.WriteLine($"Root directory: {RootDirectory}");
        }
    }
}
