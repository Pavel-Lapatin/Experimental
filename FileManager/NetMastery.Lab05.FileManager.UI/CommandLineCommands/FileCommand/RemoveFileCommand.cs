using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class RemoveFileCommand : FileCommand
    {
        public Func<FileController> Controller;
        IResultProvider _resultProvider;
        public RemoveFileCommand(Func<FileController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "remove";
            Description = "remove <path>";
            var arguments = Argument("path", "Path to remove file", false);
            OnExecute(() =>
            {
                _resultProvider.Result = Controller().Remove(arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
