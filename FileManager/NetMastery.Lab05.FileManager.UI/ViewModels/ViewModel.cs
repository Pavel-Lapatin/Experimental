using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public abstract class ViewModel
    {
        public IList<string> Messages = new List<string>();

        public virtual void RenderMessages()
        {
            if (Messages.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Messages: ");
                foreach (var message in Messages)
                {
                    Console.WriteLine(message);
                }
                Console.WriteLine();
            }
        }
        public abstract void RenderViewModel();
    }
}
