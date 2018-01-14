using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;


namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class MoveFileCommand : CommandLineApplication
    { 
        public Func<FileController> Controller;
        IResultProvider _resultProvider;
        public MoveFileCommand(Func<FileController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name ="move";
            Description = "move <path from> <path to>";
            var pathFrom = Argument("PathFrom", "Path to the moved file into virtual storage", false);
            var pathTo = Argument("PathTo", "Paths to the destination folder into virtual storage", false);
            OnExecute(() =>
            {
                try
                {
                    if (pathFrom.Value == null || pathTo.Value == null)
                    {
                        throw new CommandParsingException(this, "");
                    }
                    _resultProvider.Result = Controller().Move(pathFrom.Value, pathTo.Value);
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
