using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class RemoveDirectoryChildCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;
        public RemoveDirectoryChildCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = CommandLineNames.RemoveCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryMoveOptionNote, true);
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