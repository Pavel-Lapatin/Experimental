using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class InfoDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public InfoDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "info";
            Description = "info <path>";
            var arguments = Argument("path", "Path to directory for render Informationn", false);
            OnExecute(() =>
            {
                _resultProvider.Result = Controller().GetDirectoryInfo(arguments.Values[arguments.Values.Count - 1]); 
                return 0;
            });

        }
    }
}
