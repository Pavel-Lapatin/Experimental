using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class DownloadFileCommand : CommandLineApplication
    { 
        public Func<FileController> Controller;
        IResultProvider _resultProvider;
        public DownloadFileCommand(Func<FileController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "download";
            Description = "download <path to folder> <path to file>";
            var pathToFile = Argument("PathToFile", "Paths to the file which located into the virtual storage ", false);
            var pathToFolder = Argument("PathToFolder", "Paths to the folder located outside the virual storage", false);
            OnExecute(() =>
            {
                try
                {
                    if (pathToFile.Value == null || pathToFolder.Value == null)
                    {
                        throw new CommandParsingException(this, "");
                    }
                    using (var controller = Controller())
                    {
                        _resultProvider.Result = controller.Download(pathToFile.Value, pathToFolder.Value);
                    }
                    
                    return 0;
                }
                finally
                {
                    pathToFile.Values.Clear();
                    pathToFolder.Values.Clear();
                }
            });
        }
    }
}
