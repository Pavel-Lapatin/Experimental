using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class AddDirectoryChildCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;

        public AddDirectoryChildCommand(Func<DirectoryController> getController)
        {
            Controller = getController;

            Name = CommandLineNames.CreateCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, true);
            OnExecute(() =>
            {
                    Controller()
                    .Add(arguments.Values[arguments.Values.Count-2], 
                         arguments.Values[arguments.Values.Count-1]);
                return 0;
            });
        }
    }
}
