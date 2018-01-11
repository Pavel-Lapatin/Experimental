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
            var arguments = Argument("path", "Paths to source and destination files", true);
            OnExecute(() =>
            {
                _resultProvider.Result = Controller().Move(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
