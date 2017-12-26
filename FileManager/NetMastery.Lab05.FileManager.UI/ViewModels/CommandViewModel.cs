using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class CommandViewModel : ViewModel
    {
        public CommandViewModel() : base()
        {

        }
        public void Render(string currentPath) 
        {
            Console.Write(currentPath + "-->");
        }
    }
}
