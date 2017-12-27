using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class InfoFileCommand : CommandLine
    {
        public Func<FileController> Controller;

        public InfoFileCommand(Func<FileController> getController)
        {
            Controller = getController;
            Name = CommandLineNames.InfoCommand;

            var arguments = Argument("path", "Path to the file for rendering its info", false);
            OnExecute(() =>
            {  
                //getController().GetInfo();
                return 0;
            });
        }
    }
}
