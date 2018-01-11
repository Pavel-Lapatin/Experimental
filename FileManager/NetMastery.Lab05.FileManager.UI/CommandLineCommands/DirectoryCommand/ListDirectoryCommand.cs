using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{

    public class ListDirectoryCommand : DirectoryCommand
    { 
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public ListDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "list";
            Description = "list <path>";
            var arguments = Argument("path", "Path to directory fo render child directories and folders", false);
            OnExecute(() =>
            {
                _resultProvider.Result = Controller().ShowContent(arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
