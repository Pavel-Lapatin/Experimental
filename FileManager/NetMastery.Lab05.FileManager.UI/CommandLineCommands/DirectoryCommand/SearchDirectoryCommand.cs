using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class SearchDirectoryCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;
       
        public SearchDirectoryCommand(Func<DirectoryController> getController)
        { 
            Controller = getController;
            Name = "search";
            var arguments = Argument("path", "Path to the root directoey for recursive search", true);
            OnExecute(() =>
            {
                var form = new SearchDirectoryForm(arguments.Values[arguments.Values.Count - 2], 
                    arguments.Values[arguments.Values.Count - 1]);
                Controller().Search(form);
                return 0;
            });
        }
    }
}
