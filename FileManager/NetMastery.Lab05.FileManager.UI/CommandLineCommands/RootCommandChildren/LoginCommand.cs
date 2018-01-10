using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class LoginCommand : CommandLineApplicationRoot
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
                var l = login.Values[0];
                var p = password.Values[0];
                login.Values.Clear();
                password.Values.Clear();
                //var model = new Loginmodel(l, p);
                Controller().SinginPost(l, p);
                return 0;
            });

        }
    }
}
