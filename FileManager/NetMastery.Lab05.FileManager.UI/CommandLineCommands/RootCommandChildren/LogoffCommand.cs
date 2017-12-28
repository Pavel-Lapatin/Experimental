using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class LogoffCommand : CommandLineApplication
    {
        public Func<LoginController> Controller;

        public LogoffCommand(Func<LoginController> getController)
        {
            Controller = getController;
            Name = "logoff";
            HelpOption("-?|-h|--help");
            OnExecute(() =>
            {
               Controller().Signoff();
                return 0;
            });
        }
    }
}
