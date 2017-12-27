using NetMastery.Lab05.FileManager.UI.Controllers;
using Microsoft.Extensions.CommandLineUtils;
using System;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class RemoveDirectoryCommand : CommandLine
    {
        public Func<DirectoryController> Controller;
        public RemoveDirectoryCommand(Func<DirectoryController> getController, RedirectEvent redirectEvent) : base(redirectEvent)
        {
            Controller = getController;
            Name = CommandLineNames.RemoveCommand;
            var arguments = Argument("path", "Path to the directory for removing", true);
            OnExecute(() =>
            {

                Controller()
                .Remove(arguments.Values[arguments.Values.Count - 1]);
                
                arguments.Values.Clear();
                return 0;
            });
        }
    }
}