using NetMastery.Lab05.FileManager.UI.Controllers;
using Microsoft.Extensions.CommandLineUtils;
using System;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.Interfaces;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class AddDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public AddDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "create";
            Description ="create <path> <name>";
            Argument("path", "Path to new directory", false);
            Argument("path", "Name of new directory", false);
            OnExecute(() =>
            {
                var path = Arguments[0].Value;
                Arguments[0].Values.Clear();
                var name = Arguments[1].Value;
                Arguments[1].Values.Clear();
                _resultProvider.Result = Controller().Add(path, name);
                return 0;
            });
        }
    }
}
