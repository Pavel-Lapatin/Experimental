using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class InfoFileCommand : CommandLineApplication
    {
        public Func<FileController> Controller;
        IResultProvider _resultProvider;
        public InfoFileCommand(Func<FileController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name ="info";
            Description = "info <path>";
            var path = Argument("Path", "Path to the file into virtual storage", false);
            OnExecute(() =>
            {
                try
                {
                    if (path.Value == null)
                    {
                        throw new CommandParsingException(this, "");
                    }
                    using (var controller = Controller())
                    {
                        _resultProvider.Result = controller.GetFileInfo(path.Value);
                    }
                    return 0;
                }
                finally
                {
                    path.Values.Clear();
                }
            });
        }
    }
}
