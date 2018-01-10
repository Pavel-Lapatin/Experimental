using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{

    public class ListDirectoryCommand : DirectoryCommand
    { 
        public Func<DirectoryController> Controller;
        
        public ListDirectoryCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = "list";
            var arguments = Argument("path", "Path to directory fo render child directories and folders", false);
            OnExecute(() =>
            {
                Controller().ShowContent(arguments.Values[arguments.Values.Count - 1]);

                return 0;
            });
        }
    }
}
