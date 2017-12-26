using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.UserCommand
{
    class InfoUserCommand : CommandLineApplication
    {
        public Func<UserController> Controller;

        public InfoUserCommand(Func<UserController> getController)
        {
            Controller = getController;
            Name = CommandLineNames.InfoCommand;
            OnExecute(() =>
            {
                Controller().GetUserInfo();
                return 0;
            });
        }
       
    }
}
