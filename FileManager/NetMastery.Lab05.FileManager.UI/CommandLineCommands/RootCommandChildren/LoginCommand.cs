using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class LoginCommand : CommandLineApplication
    {
        public Func<LoginController> Controller;

        public LoginCommand(Func<LoginController> getController) 
        {
            Controller = getController;

            Name = "login";
            HelpOption("-?|-h|--help");

            var login = Option("-l | --login<value>",
                "Login  is required",
                CommandOptionType.SingleValue);
         
            var password = Option("-p|--password <value>",
                "Password is required",
                CommandOptionType.SingleValue);

            OnExecute(() =>
            {
                var form = new LoginForm(login.Values[0], password.Values[0]);
                Controller().Singin(form);
                return 0;
            });

        }
    }
}
