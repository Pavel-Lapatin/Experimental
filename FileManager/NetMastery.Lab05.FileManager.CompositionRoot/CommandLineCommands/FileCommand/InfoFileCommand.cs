using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    public class InfoFileCommand : CommandLineApplication
    {
        public Func<FileController> Controller;

        public InfoFileCommand(Func<FileController> getController)
        {
            Controller = getController;
            Name = CommandLineNames.InfoCommand;

            var arguments = Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, false);
            OnExecute(() =>
            {  
                //getController().GetInfo();
                return 0;
            });
        }
    }
}
