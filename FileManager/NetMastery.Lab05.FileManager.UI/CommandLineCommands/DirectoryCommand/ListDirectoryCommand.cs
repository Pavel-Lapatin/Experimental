using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{

    public class ListDirectoryCommand : CommandLineApplication
    { 
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public ListDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "list";
            Description = "list <path>";
            var path = Argument("path", "Path to directory fo render child directories and folders", false);
            OnExecute(() =>
            {
                try
                {
                    if (path.Value == null)
                    {
                        throw new CommandParsingException(this, "");
                    }
                    _resultProvider.Result = Controller().ShowContent(path.Value);
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
