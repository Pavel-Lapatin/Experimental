using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class SearchDirectoryChildCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;

        public SearchDirectoryChildCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = CommandLineNames.SearchCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryMoveOptionNote, true);
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
