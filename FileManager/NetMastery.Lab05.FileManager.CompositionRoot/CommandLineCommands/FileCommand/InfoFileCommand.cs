using Autofac;
using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    public class InfoFileCommand : CommandLine
    {
        public InfoFileCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.InfoCommand;

            var arguments = Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, false);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    var fileService = container.Resolve<FileController>();
                }
                return 0;
            });
        }
    }
}
