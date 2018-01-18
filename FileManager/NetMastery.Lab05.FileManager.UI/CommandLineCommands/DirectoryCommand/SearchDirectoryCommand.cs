using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class SearchDirectoryCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;
        IResultProvider _resultProvider;
        public SearchDirectoryCommand(Func<DirectoryController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "search";
            Description = "search <path> <pattern>";
            var path =Argument("path", "Path to the root directoey for recursive search", false);
            var pattern = Argument("path", "Pattern for search", false);
            OnExecute(() =>
            {
                try
                {
                    if (path.Value == null || pattern.Value == null)
                    {
                        throw new CommandParsingException(this, "");
                    }
                    using (var controller = Controller())
                    {
                        _resultProvider.Result = controller.Search(path.Value, pattern.Value);
                    }
                    return 0;
                }
                finally
                {
                    path.Values.Clear();
                    pattern.Values.Clear();
                }
            });
        }
    }
}
