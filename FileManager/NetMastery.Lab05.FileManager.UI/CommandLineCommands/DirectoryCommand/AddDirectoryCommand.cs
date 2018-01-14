using NetMastery.Lab05.FileManager.UI.Controllers;
using Microsoft.Extensions.CommandLineUtils;
using System;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.Interfaces;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class AddDirectoryCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public AddDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "create";
            Description ="create <path> <name>";
            var path = Argument("path", "Path to new directory", false);
            var name = Argument("name", "Name of new directory", false);
            OnExecute(() =>
            {
                try
                {
                    if (path.Value == null || name.Value == null)
                    {
                        throw new CommandParsingException(this, "");
                    }
                    _resultProvider.Result = Controller().Add(path.Value, name.Value);
                    return 0;
                }
                finally
                {
                    path.Values.Clear();
                    name.Values.Clear();
                }
            });
        }
    }
}
