using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.ViewModels;
using NetMastery.Lab05.FileManagerCompositionRoot;


namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands
{
    public class LoginCommand : CommandLineCommand
    {
        public LoginCommand(IContainer container, AppViewModel model) : base(container)
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
                    model.AuthenticatedLogin = container.Resolve<UserController>()
                                                        .Singin(login.Value(), password
                                                        .Value());
                }
                return 0;
            });
        }
    }
}
