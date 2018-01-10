using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class StartViewModel : ViewModel
    {
        public override void RenderViewModel()
        {
            Console.WriteLine("Welcome to File Manager v.1.0.0");
            Console.WriteLine("Contact Information: e-mail - plapatin@gmail.com");
        }
    }
}
