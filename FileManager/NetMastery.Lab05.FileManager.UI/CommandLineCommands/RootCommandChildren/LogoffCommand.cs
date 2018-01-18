using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class LogoffCommand : CommandLineApplication
    {
        public Func<LoginController> Controller;
        IResultProvider _resultProvider;
        public LogoffCommand(Func<LoginController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "logoff";
            Description = "Logoff from the system";
            OnExecute(() =>
            {
                using (var controller = Controller())
                {
                    _resultProvider.Result = controller.Signoff();
                }
                return 0;
            });
        }
    }
}
