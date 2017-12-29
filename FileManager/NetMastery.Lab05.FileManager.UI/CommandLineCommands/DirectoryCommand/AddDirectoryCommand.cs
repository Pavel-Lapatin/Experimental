using NetMastery.Lab05.FileManager.UI.Controllers;
using Microsoft.Extensions.CommandLineUtils;
using System;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class AddDirectoryCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;
       
        public AddDirectoryCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = "create";
            var arguments = Argument("path", "Path to new directory", true);
            OnExecute(() =>
            {
                //var form = new AddDirectoryForm(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                Controller().Add(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
