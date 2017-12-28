using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{

    public class ListDirectoryCommand : CommandLineApplication
    { 
        public Func<DirectoryController> Controller;
        
        public ListDirectoryCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = "list";
            var arguments = Argument("path", "Path to directory fo render child directories and folders", false);
            OnExecute(() =>
            {
                var formPath = new OnePathForm(arguments.Values[arguments.Values.Count - 1]);
                Controller().List(formPath);

                return 0;
            });
        }
    }
}
