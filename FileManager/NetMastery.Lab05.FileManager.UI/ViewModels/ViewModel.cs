using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public abstract class ViewModel
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors = new List<string>();

        public virtual void RenderErrors()
        {
            foreach (var error in Errors)
            {
                Console.WriteLine(error);
                Console.WriteLine();
            }
        }
  
    }
}
