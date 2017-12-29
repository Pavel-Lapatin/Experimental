using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public abstract class StringListViewModel : ViewModel
    {
        public IList<string> Data { get; set; }

        public StringListViewModel(IList<string> data)
        {
            Data = data;
        }
        public override void RenderViewModel()
        {
            foreach (var str in Data)
            {
                Console.WriteLine(str);
            };
            Console.WriteLine();
        }
    }
}
