using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands.DirectoryCommands
{
    public class ChangeWorkDirectoryCommand : CommandLineCommand
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
                    .ChangeWorkingDirectory(arguments.Values[0]);
                }
                return 0;
            });
        }
    }
}