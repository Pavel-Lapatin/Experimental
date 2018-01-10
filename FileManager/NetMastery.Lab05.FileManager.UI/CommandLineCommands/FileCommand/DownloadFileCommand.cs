using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class DownloadFileCommand : FileCommand
    { 
        public Func<FileController> Controller;
        public DownloadFileCommand(Func<FileController> getController)
        {  
            Controller = getController;
            Name = "download";
            var arguments = Argument("arguments", "Paths to download file and destination folder", true);
            OnExecute(() =>
            {
                //var model = new TwoPathmodel(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                Controller().Download(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
