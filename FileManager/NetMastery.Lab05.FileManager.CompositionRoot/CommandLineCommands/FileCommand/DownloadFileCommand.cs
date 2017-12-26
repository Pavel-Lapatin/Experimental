using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    public class DownloadFileCommand : CommandLineApplication
    {
        public Func<FileController> Controller;

        public DownloadFileCommand(Func<FileController> getController)
        {
            Controller = getController;
            Name = CommandLineNames.DownloadCommand;

            var arguments = Argument("arguments", EnglishLocalisation.FileDownloadOptionNote, true);
            OnExecute(() =>
            {
                Controller()
                .Download(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
