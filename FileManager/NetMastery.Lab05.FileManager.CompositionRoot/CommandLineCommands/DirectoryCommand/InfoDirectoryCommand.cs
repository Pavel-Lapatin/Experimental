using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class InfoDirectoryChildCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;

        public InfoDirectoryChildCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            
            Name = CommandLineNames.InfoCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, false);
            OnExecute(() =>
            {
                getController().GetDirectoryInfo(arguments.Values[arguments.Values.Count-1]); 
                return 0;
            });

        }
    }
}
