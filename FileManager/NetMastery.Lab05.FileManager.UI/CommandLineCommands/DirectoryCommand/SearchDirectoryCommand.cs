using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class SearchDirectoryCommand : DirectoryCommand
    {
        public Func<DirectoryController> Controller;
       
        public SearchDirectoryCommand(Func<DirectoryController> getController)
        { 
            Controller = getController;
            Name = "search";
            var arguments = Argument("path", "Path to the root directoey for recursive search", true);
            OnExecute(() =>
            {
                //var model = new SearchDirectorymodel(arguments.Values[arguments.Values.Count - 2], 
                    //arguments.Values[arguments.Values.Count - 1]);
                Controller().Search(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
