using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    public class RemoveFileCommand : CommandLineApplication
    {
        public Func<FileController> Controller;

        public RemoveFileCommand(Func<FileController> getController) 
        {
            Controller = getController;
            Name = CommandLineNames.RemoveCommand;

            var arguments = Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, false);
            OnExecute(() =>
            {
                Controller().Remove(arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
