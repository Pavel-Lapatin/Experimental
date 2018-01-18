using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class ChangeWorkDirectoryCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public ChangeWorkDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "cd";
            Description = "cd <path>";
            var path = Argument("path", "Path to current directory", false);
            OnExecute(() =>
            {
                try
                {
                    if (path.Value == null)
                    {
                        throw new CommandParsingException(this, "");
                    }
                    using (var controller = Controller())
                    {
                        _resultProvider.Result = controller.ChangeWorkingDirectory(path.Value);
                    }
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