using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class RemoveDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public RemoveDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "remove";
            Description = "remove <path>";
            var arguments = Argument("path", "Path to the directory for removing", true);
            OnExecute(() =>
            {
                _resultProvider.Result = Controller().Remove(arguments.Values[arguments.Values.Count - 1]);    
                return 0;
            });
        }
    }
}