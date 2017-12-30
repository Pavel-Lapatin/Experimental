using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Forms
{
    public class StartForm : Form
    {
        const string WelcomeNote = "Welcome to File Manager v.1.0.0";
        const string ContactInformation = "Contact information: e-mail - plapatin@gmail.com";
        public StartForm(string currentPath) : base(currentPath)
        {
        }
        public void Render()
        {
            Console.WriteLine(WelcomeNote);
            Console.WriteLine(ContactInformation);
            Console.WriteLine();
        }
    }
}
