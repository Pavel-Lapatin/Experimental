using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class ChangeWorkDirectoryCommand : CommandLineApplication
    {
        public Func<DirectoryController> Controller;
        public ChangeWorkDirectoryCommand(Func<DirectoryController> getController)
        {
            Controller = getController;
            Name = "cd";
            var arguments = Argument("path", "Path to current directory", true);
            OnExecute(() =>
            {
                var form = new OnePathForm(arguments.Values[arguments.Values.Count - 1]);
                Controller().ChangeWorkingDirectory(form);
                return 0;
            });
        }
    }
}