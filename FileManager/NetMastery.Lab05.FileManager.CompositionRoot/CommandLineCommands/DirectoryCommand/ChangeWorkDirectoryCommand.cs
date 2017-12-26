using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;
using System;
using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class ChangeWorkDirectoryChildCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;

        public ChangeWorkDirectoryChildCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = CommandLineNames.ChangeDirectoryCommand;
            var arguments = Argument("path", EnglishLocalisation.ChangeDirectoryOptionNote, true);
            OnExecute(() =>
            {

                Controller()
                .ChangeWorkingDirectory(arguments.Values[arguments.Values.Count-1]);
                return 0;
            });
        }
    }
}