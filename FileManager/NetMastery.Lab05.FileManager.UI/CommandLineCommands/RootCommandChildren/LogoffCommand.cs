using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class LogoffCommand : CommandLineApplicationRoot
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
                _resultProvider.Result = Controller().Signoff();
                return 0;
            });
        }
    }
}
