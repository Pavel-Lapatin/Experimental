using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class MoveDirectoryCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;
        
        public MoveDirectoryCommand(Func<DirectoryController> getController)
        {
            
            Controller = getController;
            Name = "move";
            var arguments = Argument("path", "Paths for source and destination directories", true);
            OnExecute(() =>
            {
                var form = new TwoPathForm(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                Controller().Move(form);
                arguments.Values.Clear();
                return 0;
            });
        }
    }
}