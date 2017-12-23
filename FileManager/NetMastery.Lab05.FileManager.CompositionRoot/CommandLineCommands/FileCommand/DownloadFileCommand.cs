using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands
{
    public class DownloadFileCommand : CommandLineCommand
    {
        public DownloadFileCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.FileCommand;

            Command(CommandLineNames.DownloadOption, c =>
            {
                var arguments = c.Argument("arguments", EnglishLocalisation.FileDownloadOptionNote, true);
                c.OnExecute(() =>
                {
                    using (var scope = _container.BeginLifetimeScope())
                    {
                        container.Resolve<FileController>()
                        .Download(arguments.Values[0], arguments.Values[1]);
                    }
                    return 0;
                });
            });
        }
    }
}
