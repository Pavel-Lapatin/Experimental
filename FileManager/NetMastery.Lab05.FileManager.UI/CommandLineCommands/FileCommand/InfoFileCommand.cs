using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class InfoFileCommand : CommandLineApplication
    {
        public Func<FileController> Controller;
        
        public InfoFileCommand(Func<FileController> getController)
        { 
            Controller = getController;
            Name ="info";

            var arguments = Argument("path", "Path to the file for rendering its info", false);
            OnExecute(() =>
            {
                //var model = new OnePathmodel(arguments.Values[arguments.Values.Count - 1]);
                Controller().GetFileInfo(arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
