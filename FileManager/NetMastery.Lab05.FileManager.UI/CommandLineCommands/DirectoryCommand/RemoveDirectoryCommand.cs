using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class RemoveDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
        public RemoveDirectoryCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = "remove";
            var arguments = Argument("path", "Path to the directory for removing", true);
            OnExecute(() =>
            {
                //var model = new OnePathmodel(arguments.Values[arguments.Values.Count - 1]);
                Controller().Remove(arguments.Values[arguments.Values.Count - 1]);    
                return 0;
            });
        }
    }
}