using Autofac;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    public class RemoveFileCommand : CommandLine
    {
        public RemoveFileCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.RemoveCommand;

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
