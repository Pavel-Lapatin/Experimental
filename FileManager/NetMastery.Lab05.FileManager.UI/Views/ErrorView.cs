using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Views
{
    public class ErrorView : View
    {
        private IDictionary<string, IList<string>> _errors;
        public ErrorView(IDictionary<string, IList<string>> errors)
        {
            _errors = errors;
        }
        public override void RenderView()
        {
            foreach (var key in _errors)
            {
                Console.WriteLine($"Errors assosiated with {key.Key}:");
                foreach (var error in _errors[key.Key])
                {
                    Console.WriteLine(error);
                }
            }
        }
    }
}
