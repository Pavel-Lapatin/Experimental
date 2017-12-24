using Autofac;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class SearchDirectoryCommand : CommandLine
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
                    .Search(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                }
                arguments.Values.Clear();
                return 0;
            });
        }
    }
}
