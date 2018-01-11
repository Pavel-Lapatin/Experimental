using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class DownloadFileCommand : FileCommand
    { 
        public Func<FileController> Controller;
        IResultProvider _resultProvider;
        public DownloadFileCommand(Func<FileController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "download";
            Description = "download <path to file> <path to folder>";
            var arguments = Argument("arguments", "Paths to download file and destination folder", true);
            OnExecute(() =>
            {
                _resultProvider.Result = Controller().Download(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
