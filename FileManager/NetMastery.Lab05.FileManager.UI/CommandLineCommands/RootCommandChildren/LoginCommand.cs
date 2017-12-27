using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class LoginCommand : CommandLine
    {
        public Func<LoginViewModel, LoginController> Controller;

        public LoginCommand(Func<LoginViewModel, LoginController> getController, RedirectEvent redirectEvent) : base(redirectEvent)
        {
            Controller = getController;

            Name = CommandLineNames.LoginCommand;
            HelpOption(CommandLineNames.HelpOption);

            var login = Option(CommandLineNames.LoginOption,
                "Login  is required",
                CommandOptionType.SingleValue);

            var password = Option(CommandLineNames.PasswordOption,
                "Password is required",
                CommandOptionType.SingleValue);

            OnExecute(() =>
            {
                var loginVM = new LoginViewModel(login.Value(), password.Value());
                var controller = Controller(loginVM);
                controller.Redirect.Redirected += RedirecteRedirectEventHandler;
                controller.Singin();
                Options[1].Values.Clear();
                Options[2].Values.Clear();
                return 0;
            });

        }
    }
}
