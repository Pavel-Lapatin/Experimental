using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class UploadFileCommand : FileCommand
    {
        public Func<FileController> Controller;
        public UploadFileCommand(Func<FileController> getController)
        {  
            Controller = getController;
            Name = "upload";
            var arguments = Argument("path", "Paths", true);
            OnExecute(() => 
            {
                Controller().Upload(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
