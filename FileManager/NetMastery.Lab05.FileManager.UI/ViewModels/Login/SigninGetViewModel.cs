using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class SigninGetViewModel : ViewModel
    {
        public override void RenderViewModel()
        {
            Console.WriteLine("Please, signin in the system");
            Console.WriteLine("Command: login -l <userName> -p <password>");
        }
    }
}
