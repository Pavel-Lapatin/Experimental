using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Views
{
    public class SearchView : View
    {
        private readonly IList<string> _result;
        public SearchView(IList<string> result)
        {
            _result = result;
        }

        public override void RenderView()
        {
            if(_result != null)
            {
                Console.WriteLine();
                Console.WriteLine("Search result:");

                foreach (var item in _result)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
            }
          
        }
    }
}
