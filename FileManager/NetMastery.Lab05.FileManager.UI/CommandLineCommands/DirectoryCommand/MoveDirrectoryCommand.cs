using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class MoveDirectoryCommand : CommandLine
    {
        public Func<DirectoryController> Controller;

        public MoveDirectoryCommand(Func<DirectoryController> getController) 
        {
            Controller = getController;
            Name = CommandLineNames.MoveCommand;
            var arguments = Argument("path", "Paths for source and destination directories", true);
            OnExecute(() =>
            {

                Controller()
                .Move(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                arguments.Values.Clear();
                return 0;
            });
        }
    }
}