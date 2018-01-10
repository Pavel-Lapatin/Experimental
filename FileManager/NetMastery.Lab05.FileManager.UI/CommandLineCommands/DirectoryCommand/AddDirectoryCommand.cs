using NetMastery.Lab05.FileManager.UI.Controllers;
using Microsoft.Extensions.CommandLineUtils;
using System;
using NetMastery.Lab05.FileManager.UI.Results;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class AddDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
       
        public AddDirectoryCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = "create";
            var arguments = Argument("path", "Path to new directory", true);
            OnExecute(() =>
            {
                var controlller = Controller();
                Controller().Add(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
