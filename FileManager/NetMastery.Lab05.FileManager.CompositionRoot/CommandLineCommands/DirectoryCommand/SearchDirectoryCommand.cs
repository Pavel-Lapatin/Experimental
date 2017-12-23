using Autofac;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands.DirectoryCommands
{
    public class SearchDirectoryCommand : CommandLineCommand
    {
        public SearchDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.SearchCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryMoveOptionNote, true);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    container.Resolve<DirectoryController>()
                    .Search(arguments.Values[0], arguments.Values[1]);
                }
                return 0;
            });
        }
    }
}
