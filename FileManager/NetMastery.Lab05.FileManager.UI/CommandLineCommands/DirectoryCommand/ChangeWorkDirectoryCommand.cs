using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class ChangeWorkDirectoryCommand : CommandLine
    {
        public Func<DirectoryController> Controller;

        public ChangeWorkDirectoryCommand(Func<DirectoryController> getController, RedirectEvent redirectEvent) : base(redirectEvent)
        {
            Controller = getController;
            Name = CommandLineNames.ChangeDirectoryCommand;
            var arguments = Argument("path", "Path to current directory", true);
            OnExecute(() =>
            {

                Controller()
                .ChangeWorkingDirectory(arguments.Values[arguments.Values.Count-1]);
                return 0;
            });
        }
    }
}