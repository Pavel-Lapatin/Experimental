using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using NetMastery.Lab05.FileManager.UI.Results;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class LoginCommand : CommandLineApplication
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
            var login = Option("-l|--login<value>", "Login  is required", CommandOptionType.SingleValue);
            var password = Option("-p|--password <value>", "Password is required", CommandOptionType.SingleValue);
            OnExecute(() =>
            {
                try
                {
                    if (!login.HasValue() || !password.HasValue())
                    {
                        throw new CommandParsingException(this, "");
                    }
                    _resultProvider.Result = Controller().SinginPost(login.Value(), password.Value());
                    return 0;
                } finally
                {
                    login.Values.Clear();
                    password.Values.Clear();
                }        
            });
        }
    }
}
