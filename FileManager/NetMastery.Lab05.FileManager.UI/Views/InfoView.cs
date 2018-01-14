using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Views
{
    public class InfoView : View
    {
        private readonly string _successMessage;
        public InfoView(string successMessage)
        {
            _successMessage = successMessage;
        }

        public override void RenderView()
        {
           Console.WriteLine(_successMessage);
        }
    }
}
