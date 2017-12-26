using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{

    public class ListDirectoryChildCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;

        public ListDirectoryChildCommand(Func<DirectoryController> getController) 
        {
            Controller = getController;

            Name = CommandLineNames.ListCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryMoveOptionNote, false);
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
