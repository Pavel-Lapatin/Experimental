using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class RemoveFileCommand : FileCommand
    {
        public Func<FileController> Controller;
        public RemoveFileCommand(Func<FileController> getController)
        {
            Controller = getController;
            Name = "remove";
            var arguments = Argument("path", "Path to remove file", false);
            OnExecute(() =>
            {
                //var model = new OnePathmodel(arguments.Values[arguments.Values.Count - 1]);
                Controller().Remove(arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
