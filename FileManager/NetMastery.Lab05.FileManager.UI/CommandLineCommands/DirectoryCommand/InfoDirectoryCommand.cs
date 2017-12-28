using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class InfoDirectoryCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;

        public InfoDirectoryCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            
            Name = "info";
            var arguments = Argument("path", "Path to directory for render informationn", false);
            OnExecute(() =>
            {
                var form = new OnePathForm(arguments.Values[arguments.Values.Count - 1]);
                Controller().GetDirectoryInfo(form); 
                return 0;
            });

        }
    }
}
