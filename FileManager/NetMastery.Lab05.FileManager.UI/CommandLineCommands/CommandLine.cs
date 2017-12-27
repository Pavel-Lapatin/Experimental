using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class CommandLine  : CommandLineApplication
    {
        public RedirectEvent Redirected;
        public CommandLine(RedirectEvent redirectEvent)
        {
            Redirected = redirectEvent;
        }
        public void RedirecteRedirectEventHandler(object sender, RedirectEventArgs e)
        {
            Redirected.OnRedirect(this, e);
        }
    }
}
