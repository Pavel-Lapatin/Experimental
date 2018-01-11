using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class SearchDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public SearchDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "search";
            Description = "search <path> <pattern>";
            var arguments = Argument("path", "Path to the root directoey for recursive search", true);
            OnExecute(() =>
            {
                _resultProvider.Result = Controller().Search(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
