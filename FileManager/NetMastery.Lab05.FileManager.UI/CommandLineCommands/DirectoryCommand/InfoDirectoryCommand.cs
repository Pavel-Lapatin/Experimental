using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class InfoDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
        public InfoDirectoryCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = "info";
            var arguments = Argument("path", "Path to directory for render Informationn", false);
            OnExecute(() =>
            {
                //var model = new OnePathmodel(arguments.Value);
                Controller().GetDirectoryInfo(arguments.Values[arguments.Values.Count - 1]); 
                return 0;
            });

        }
    }
}
