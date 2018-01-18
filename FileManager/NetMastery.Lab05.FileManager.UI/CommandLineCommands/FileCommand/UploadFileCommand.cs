using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class UploadFileCommand : CommandLineApplication
    {
        public Func<FileController> Controller;
        IResultProvider _resultProvider;
        public UploadFileCommand(Func<FileController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "upload";
            Description = "upload <path to file> <path to folder>";
            var pathToFile = Argument("PathToFile", "Paths to the file which located outside the virtual storage ", false);
            var pathToFolder = Argument("PathToFolder", "Paths to the folder located into the virual storage", false);
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
                        _resultProvider.Result = controller.Upload(pathToFile.Value, pathToFolder.Value);
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
