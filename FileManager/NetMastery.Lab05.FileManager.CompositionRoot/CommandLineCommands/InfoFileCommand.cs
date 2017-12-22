using Autofac;
using NetMastery.FileManeger.Bl.Interfaces;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManagerCompositionRoot.CommandLineCommands
{
    public class InfoFileCommand : CommandLineCommand
    {
        public InfoFileCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.UserCommand;

            Command(CommandLineNames.InfoCommand, c =>
            {
                var arguments = c.Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, false);
                c.OnExecute(() =>
                {
                    using (var scope = _container.BeginLifetimeScope())
                    {
                        var fileService = container.Resolve<FileController>();
                        var writer = container.Resolve<IInfoWriter<FileStructureDto>>();
                        writer.WriteInfo(fileService.GetDirectoryByPath(arguments.Value));
                    }
                    return 0;
                });
            });

        }
    }
}
