using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class LogoffCommand : CommandLine
    {
        public Func<LoginController> Controller;

        public LogoffCommand(Func<LoginController> getController)
        {
            Controller = getController;
            Name = CommandLineNames.LogoffCommand;
            HelpOption(CommandLineNames.HelpOption);


            OnExecute(() =>
            {
               Controller().Signoff();
                return 0;
            });
        }
    }
}
