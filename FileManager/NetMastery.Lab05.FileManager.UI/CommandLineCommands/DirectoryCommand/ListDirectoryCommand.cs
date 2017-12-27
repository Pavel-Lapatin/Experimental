using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{

    public class ListDirectoryCommand : CommandLine
    { 
        public Func<DirectoryController> Controller;

        public ListDirectoryCommand(Func<DirectoryController> getController, RedirectEvent redirectEvent) : base(redirectEvent)
        {
            Controller = getController;

            Name = CommandLineNames.ListCommand;
            var arguments = Argument("path", "Path to directory fo render child directories and folders", false);
            OnExecute(() =>
            {
                   Controller()
                   .List(arguments.Values[arguments.Values.Count-1]);

                arguments.Values.Clear();
                return 0;
            });
        }
    }
}
