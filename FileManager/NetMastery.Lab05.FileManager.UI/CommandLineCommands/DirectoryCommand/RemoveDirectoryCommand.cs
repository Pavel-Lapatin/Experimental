using NetMastery.Lab05.FileManager.UI.Controllers;
using Microsoft.Extensions.CommandLineUtils;
using System;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class RemoveDirectoryCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;
        public RemoveDirectoryCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = "remove";
            var arguments = Argument("path", "Path to the directory for removing", true);
            OnExecute(() =>
            {
                var form = new OnePathForm(arguments.Values[arguments.Values.Count - 1]);
                Controller().Remove(form);
                
                return 0;
            });
        }
    }
}