using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class ChangeWorkDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
        public ChangeWorkDirectoryCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = "cd";
            var arguments = Argument("path", "Path to current directory", true);
            OnExecute(() =>
            {
                //var model = new OnePathmodel(arguments.Values[arguments.Values.Count - 1]);
                Controller().ChangeWorkingDirectory(arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}