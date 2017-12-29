using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class UploadFileCommand : CommandLineApplication
    {
        public Func<FileController> Controller;
        public UploadFileCommand(Func<FileController> getController)
        {  
            Controller = getController;
            Name = "upload";
            var arguments = Argument("path", "Paths", true);
            OnExecute(() => 
            {
                //var form = new TwoPathForm(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                Controller().Upload(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
