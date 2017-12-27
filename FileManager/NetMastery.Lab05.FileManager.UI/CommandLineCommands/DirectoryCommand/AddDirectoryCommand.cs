using NetMastery.Lab05.FileManager.UI.Controllers;
using Microsoft.Extensions.CommandLineUtils;
using System;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class AddDirectoryCommand : CommandLine
    {
        public Func<DirectoryController> Controller;

        public AddDirectoryCommand(Func<DirectoryController> getController, RedirectEvent redirectEvent) : base(redirectEvent)
        {
            Controller = getController;

            Name = CommandLineNames.CreateCommand;
            var arguments = Argument("path", "Path to new directory", true);
            OnExecute(() =>
            {
                Controller()
                .Add(arguments.Values[arguments.Values.Count - 2],
                     arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
