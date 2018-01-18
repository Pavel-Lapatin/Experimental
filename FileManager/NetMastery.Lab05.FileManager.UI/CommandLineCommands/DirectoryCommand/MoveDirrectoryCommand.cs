using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class MoveDirectoryCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public MoveDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "move";
            Description = "move <path of the folder you want to move> <path to move>";
            var pathFrom = Argument("pathFrom", "Path to the moved folder", false);
            var pathTo = Argument("pathTo", "Paths to the destination folder", false);
            OnExecute(() =>
            {
                try
                {
                    if (pathFrom.Value == null || pathTo.Value == null)
                    {
                        throw new CommandParsingException(this, "");
                    }
                    using (var controller = Controller())
                    {
                        _resultProvider.Result = controller.Move(pathFrom.Value, pathTo.Value);
                    }
                    return 0;
                }
                finally
                {
                    pathFrom.Values.Clear();
                    pathTo.Values.Clear();
                }
            });
        }
    }
}