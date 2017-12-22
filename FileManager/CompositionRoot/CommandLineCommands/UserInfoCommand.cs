using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.BLModel.DtoClasses;
using NetMastery.Lab05.FileManager.ViewModels;
using NetMastery.Lab05.FileManagerCompositionRoot;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands
{
    public class UserInfoCommand : CommandLineCommand
    {
        public UserInfoCommand(IContainer container, AppViewModel model) : base(container)
        {

            Name = CommandLineNames.UserCommand;
            HelpOption(CommandLineNames.HelpOption);

            var info = Option(CommandLineNames.InfoOption, 
                EnglishLocalisation.DirectoryCreateOptionNote, 
                CommandOptionType.NoValue);

            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    var userService = container.Resolve<UserController>();
                    var writer = container.Resolve<IInfoWriter<UserInfo>>();
                    writer.WriteInfo(userService.GetUserInfo(model.AuthenticatedLogin));
                }
                return 0;
            });
        }
    }
}
