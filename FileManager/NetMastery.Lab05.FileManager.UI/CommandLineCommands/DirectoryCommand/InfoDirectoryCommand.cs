using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class InfoDirectoryCommand : CommandLine
    {
        public Func<DirectoryController> Controller;

        public InfoDirectoryCommand(Func<DirectoryController> getController, RedirectEvent redirectEvent) : base(redirectEvent)
        {
            Controller = getController;
            
            Name = CommandLineNames.InfoCommand;
            var arguments = Argument("path", "Path to directory for render informationn", false);
            OnExecute(() =>
            {
                getController().GetDirectoryInfo(arguments.Values[arguments.Values.Count-1]); 
                return 0;
            });

        }
    }
}
