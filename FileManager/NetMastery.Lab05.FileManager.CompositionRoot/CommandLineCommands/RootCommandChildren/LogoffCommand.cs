using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.RootCommandChildren
{

    public class LogoffRootChildCommand : CommandLineApplication
    {
        public Func<LoginController> Controller;

        public LogoffRootChildCommand(Func<LoginController> getController)
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
