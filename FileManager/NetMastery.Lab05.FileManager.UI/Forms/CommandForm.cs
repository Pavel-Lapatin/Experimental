using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Forms
{
    public class CommandForm : Form
    {
        public string CurrentPath { get; set; }
        public override void RenderForm()
        {
            Console.Write(CurrentPath + "-->");
        }
    }
}
