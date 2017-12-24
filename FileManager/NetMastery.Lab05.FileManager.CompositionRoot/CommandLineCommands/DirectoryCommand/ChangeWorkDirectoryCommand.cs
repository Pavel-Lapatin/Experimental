using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class ChangeWorkDirectoryCommand : CommandLine
    {
        public ChangeWorkDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.ChangeDirectoryCommand;
            var arguments = Argument("path", EnglishLocalisation.ChangeDirectoryOptionNote, true);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    container.Resolve<DirectoryController>()
                    .ChangeWorkingDirectory(arguments.Values[arguments.Values.Count-1]);
                }
                return 0;
            });
        }
    }
}