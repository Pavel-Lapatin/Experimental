using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.events
{
    public delegate void RedirectEventHandler(object sender, RedirectEventArgs e);

    public class RedirectEvent
    {
        public event RedirectEventHandler Redirected;


        public void OnRedirect(object sender, RedirectEventArgs e)
        {
            Redirected(sender, e);
        }
    }
}
