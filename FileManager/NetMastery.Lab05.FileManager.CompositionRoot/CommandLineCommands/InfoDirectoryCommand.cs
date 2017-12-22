using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.FileManeger.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.ViewModels;
using NetMastery.Lab05.FileManagerCompositionRoot;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands
{
    public class InfoDirectoryCommand : CommandLineCommand
    {
        public InfoDirectoryCommand(IContainer container, AppViewModel model) : base(container)
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
                    var directoryController = container.Resolve<DirectoryController>();
                    var writer = container.Resolve<IInfoWriter<DirectoryStructureDto>>();
                    writer.WriteInfo(directoryController.GetDirectoryByPath(model.CurrentPath));
                }
                return 0;
            });
        }
    }
}
