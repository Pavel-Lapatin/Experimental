using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class SearchDirectoryCommand : CommandLine
    {
        public Func<DirectoryController> Controller;

        public SearchDirectoryCommand(Func<DirectoryController> getController, RedirectEvent redirectEvent) : base(redirectEvent)
        {
            Controller = getController;
            Name = CommandLineNames.SearchCommand;
            var arguments = Argument("path", "Path to the root directoey for recursive search", true);
            OnExecute(() =>
            {
                Controller()
                .Search(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                arguments.Values.Clear();
                return 0;
            });
        }
    }
}
