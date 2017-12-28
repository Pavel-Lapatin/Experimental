using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using System;


namespace NetMastery.Lab05.FileManager.UI.Commands
{
    class MoveFileCommand : CommandLineApplication
    { 
        public Func<FileController> Controller;
        public MoveFileCommand(Func<FileController> getController)
        { 
            Controller = getController;
            Name ="move";
            var arguments = Argument("path", "Paths to source and destination files", false);
            OnExecute(() =>
            {
                var form = new TwoPathForm(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                Controller().Move(form);
                return 0;
            });
        }
    }
}
