using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class RemoveFileCommand : CommandLineApplication
    {
        public Func<FileController> Controller;
        IResultProvider _resultProvider;
        public RemoveFileCommand(Func<FileController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "remove";
            Description = "remove <path>";
            var path = Argument("Path", "Path to file into virtual storage", false);
            OnExecute(() =>
            {
                try
                {
                    if (path.Value == null)
                    {
                        throw new CommandParsingException(this, "");
                    }
                    _resultProvider.Result = Controller().Remove(path.Value);
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
