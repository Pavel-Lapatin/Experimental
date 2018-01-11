using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class ChangeWorkDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public ChangeWorkDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "cd";
            Description = "cd <path>";
            Argument("path", "Path to current directory", false);
            OnExecute(() =>
            {
                var path = Arguments[0].Value;
                Arguments[0].Values.Clear();

                _resultProvider.Result = Controller().ChangeWorkingDirectory(path);
                return 0;
            });
        }
    }
}