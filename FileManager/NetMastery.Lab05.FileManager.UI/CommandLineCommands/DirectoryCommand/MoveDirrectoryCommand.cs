using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class MoveDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
        
        public MoveDirectoryCommand(Func<DirectoryController> getController)
        {
            
            Controller = getController;
            Name = "move";
            var arguments = Argument("path", "Paths for source and destination directories", true);
            OnExecute(() =>
            {
                //var model = new TwoPathmodel(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                Controller().Move(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}