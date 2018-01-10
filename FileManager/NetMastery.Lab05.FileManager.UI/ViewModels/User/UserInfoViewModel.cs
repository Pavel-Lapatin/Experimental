using NetMastery.Lab05.FileManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class UserInfoViewModel : ViewModel
    {
        public AccountDto Account { get; set; }

        public override void RenderViewModel()
        {
            if (Account != null)
            {
                base.RenderViewModel();
                Console.WriteLine();
                Console.WriteLine("User info: ");
                Console.WriteLine($"Login: {Account.Login}");
                Console.WriteLine($"Creation date: {Account.CreationDate.ToString("yy-MM-dd")}");
                Console.WriteLine($"Current Storage Size: {Account.CurentStorageSize}");
                Console.WriteLine($"Max Storage Size: {Account.MaxStorageSize }");
                Console.WriteLine($"Root directory: {Account.RootDirectory}");
                Console.WriteLine();
            }
        }
    }
}
