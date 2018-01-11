using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using NetMastery.Lab05.FileManager.UI.Results;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class LoginCommand : CommandLineApplicationRoot
    {
        public Func<LoginController> Controller;
        private IResultProvider _resultProvider;
        public LoginCommand(Func<LoginController> getController, IResultProvider resultProvider) 
        {
            _resultProvider = resultProvider;
            Controller = getController;
            _resultProvider = resultProvider;
            Name = "login";
            Description = "Login to the system";
            Option("-l|--login<value>", "Login  is required", CommandOptionType.SingleValue);
            Option("-p|--password <value>", "Password is required", CommandOptionType.SingleValue);
            OnExecute(() =>
            {
                var login = Options[1].Values[0];
                Options[1].Values.Clear();
                var password = Options[2].Values[0];
                Options[2].Values.Clear();
                _resultProvider.Result = Controller().SinginPost(login, password);
                return 0;
            });
        }
    }
}
