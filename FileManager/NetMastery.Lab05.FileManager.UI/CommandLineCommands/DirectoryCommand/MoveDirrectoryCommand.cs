using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class MoveDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public MoveDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "move";
            Description = "move <path of the folder you want to move> <path to move>";
            var arguments = Argument("path", "Paths for source and destination directories", true);
            OnExecute(() =>
            {
                _resultProvider.Result = Controller().Move(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}