using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class InfoUserCommand : CommandLineApplication
    {
        public Func<UserController> Controller;

        public InfoUserCommand(Func<UserController> getController)
        {
            Controller = getController;
            Name = "info";
            OnExecute(() =>
            {
                Controller().GetUserInfo();
                return 0;
            });
        }
       
    }
}
