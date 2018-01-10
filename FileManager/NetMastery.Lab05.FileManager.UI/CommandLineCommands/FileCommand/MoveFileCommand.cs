using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;


namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class MoveFileCommand : CommandLineApplication
    { 
        public Func<FileController> Controller;
        public MoveFileCommand(Func<FileController> getController)
        { 
            Controller = getController;
            Name ="move";
            var arguments = Argument("path", "Paths to source and destination files", true);
            OnExecute(() =>
            {
                //var model = new TwoPathmodel(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                Controller().Move(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
