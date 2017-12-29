using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Forms
{
    public class CommandForm : Form
    {
        public CommandForm(string currentPath) : base(currentPath)
        {

        }
        public override void RenderForm()
        {
            Console.Write(_currentPath + "-->");
        }
    }
}
