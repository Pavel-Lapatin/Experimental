using NetMastery.Lab05.FileManager.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Results
{
    public class ContentView : View
    {
        private IDictionary<string, IList<string>> _content;

        public ContentView(IDictionary<string, IList<string>> content)
        {
            _content = content;
        }
        public override void RenderView()
        {
            foreach (var key in _content)
            {
                Console.WriteLine();     
                Console.WriteLine($"{key.Key}:");
                foreach (var item in _content[key.Key])
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine();
        }
    }

}
