using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;


namespace NetMastery.Lab05.FileManager.UI.Commands
{
    class MoveFileCommand : CommandLine
    { 
        public Func<FileController> Controller;

        public MoveFileCommand(Func<FileController> getController) 
        {
            Controller = getController;

            Name = CommandLineNames.InfoCommand;

            var arguments = Argument("path", "Paths to source and destination files", false);
            OnExecute(() =>
            {
                getController().Move(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
