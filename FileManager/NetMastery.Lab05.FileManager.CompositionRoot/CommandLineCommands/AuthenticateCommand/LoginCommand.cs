using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.AuthenticateCommand
{

    public class LoginCommand : CommandLine
    {
        public LoginCommand(IContainer container) : base(container)
        {
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
                using (var scope = _container.BeginLifetimeScope())
                {
                    var contr = container.Resolve<LoginController>();
                    contr.Singin(login.Value(), password.Value());
                }
                Options[1].Values.Clear();
                Options[2].Values.Clear();
                return 0;
            });

        }
    }
}
