using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;
using System;
using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class MoveDirectoryChildCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;

        public MoveDirectoryChildCommand(Func<DirectoryController> getController) 
        {
            Controller = getController;
            Name = CommandLineNames.MoveCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryMoveOptionNote, true);
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