using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    public class UploadFileCommand : CommandLineApplication
    {
        public Func<FileController> Controller;

        public UploadFileCommand(Func<FileController> getController)
        {
            Controller = getController;
            Name = CommandLineNames.UploadCommand;

            var arguments = Argument("path", EnglishLocalisation.FileUploadOptionNote, true);
            OnExecute(() =>
            {
                Controller()
                .Upload(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
