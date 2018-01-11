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
            var arguments = Argument("path", "Path to the file for rendering its info", false);
            OnExecute(() =>
            {
                _resultProvider.Result = Controller().GetFileInfo(arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
