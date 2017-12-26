using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    class MoveFileCommand : CommandLineApplication
    {
        public Func<FileController> Controller;

        public MoveFileCommand(Func<FileController> getController) 
        {
            Controller = getController;

            Name = CommandLineNames.InfoCommand;

            var arguments = Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, false);
            OnExecute(() =>
            {
                getController().Move(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
