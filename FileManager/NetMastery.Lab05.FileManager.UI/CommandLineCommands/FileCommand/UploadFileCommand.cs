using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class UploadFileCommand : FileCommand
    {
        public Func<FileController> Controller;
        IResultProvider _resultProvider;
        public UploadFileCommand(Func<FileController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "upload";
            Description = "upload <path to file> <path to folder>";
            var arguments = Argument("path", "Paths", true);
            OnExecute(() => 
            {
                _resultProvider.Result = Controller().Upload(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
