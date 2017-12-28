using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class DownloadFileCommand : CommandLineApplication
    { 
        public Func<FileController> Controller;

        public DownloadFileCommand(Func<FileController> getController)
        {
            Controller = getController;
            Name = "download";

            var arguments = Argument("arguments", "Paths to download file and destination folder", true);
            OnExecute(() =>
            {
                var form = new TwoPathForm(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                Controller().Download(form);
                return 0;
            });
        }
    }
}
