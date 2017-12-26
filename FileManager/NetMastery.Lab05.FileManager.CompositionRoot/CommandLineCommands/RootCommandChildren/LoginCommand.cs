using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.RootCommandChildren
{

    public class LoginRootChildCommand : CommandLineApplication
    {
        public Func<LoginViewModel, LoginController> Controller;

        public LoginRootChildCommand(Func<LoginViewModel, LoginController> getController)
        {
            Controller = getController;

            Name = CommandLineNames.LoginCommand;
            HelpOption(CommandLineNames.HelpOption);

            var login = Option(CommandLineNames.LoginOption,
                EnglishLocalisation.LoginOptionHelpNote,
                CommandOptionType.SingleValue);

            var password = Option(CommandLineNames.PasswordOption,
                EnglishLocalisation.PasswordOptionHelpNote,
                CommandOptionType.SingleValue);

            OnExecute(() =>
            {
                var loginVM = new LoginViewModel(login.Value(), password.Value());
                var controller = Controller(loginVM); //as LoginController;
                controller.Singin();
                Options[1].Values.Clear();
                Options[2].Values.Clear();
                return 0;
            });

        }
    }
}
