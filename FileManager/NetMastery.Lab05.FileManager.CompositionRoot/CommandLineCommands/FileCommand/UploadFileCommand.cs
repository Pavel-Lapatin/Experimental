using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    public class UploadFileCommand : CommandLine
    {
        public UploadFileCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.UploadCommand;

            var arguments = Argument("path", EnglishLocalisation.FileUploadOptionNote, true);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    container.Resolve<FileController>()
                    .Upload(arguments.Values[0], arguments.Values[1]);
                }
                return 0;
            });
        }
    }
}
