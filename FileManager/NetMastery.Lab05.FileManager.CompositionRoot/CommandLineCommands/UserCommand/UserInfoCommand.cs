using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands
{
    public class UserInfoCommand : CommandLineCommand
    {
        public UserInfoCommand(IContainer container) : base(container)
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
                    container.Resolve<UserController>().GetUserInfo(); ;   
                }
                return 0;
            });
        }
    }
}
