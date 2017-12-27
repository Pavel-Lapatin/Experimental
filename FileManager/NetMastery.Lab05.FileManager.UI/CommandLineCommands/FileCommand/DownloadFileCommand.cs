using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class DownloadFileCommand : CommandLine
    { 
        public Func<FileController> Controller;

        public DownloadFileCommand(Func<FileController> getController)
        {
            Controller = getController;
            Name = CommandLineNames.DownloadCommand;

            var arguments = Argument("arguments", "Paths to download file and destination folder", true);
            OnExecute(() =>
            {
                Controller()
                .Download(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
