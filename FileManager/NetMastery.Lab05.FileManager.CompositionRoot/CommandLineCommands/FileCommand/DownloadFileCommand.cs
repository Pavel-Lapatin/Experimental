using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    public class DownloadFileCommand : CommandLine
    {
        public DownloadFileCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.DownloadCommand;

            var arguments = Argument("arguments", EnglishLocalisation.FileDownloadOptionNote, true);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    container.Resolve<FileController>()
                    .Download(arguments.Values[0], arguments.Values[1]);
                }
                return 0;
            });
        }
    }
}
