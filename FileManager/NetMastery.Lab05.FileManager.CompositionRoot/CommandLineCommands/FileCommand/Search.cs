using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;



namespace CompositionRoot.CommandLineCommands
{
    public class SearchDirectoryCommand : CommandLineCommand
    {
        public SearchDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.DirectoryCommand;

            Command(CommandLineNames.MoveOption, c =>
            {
                var arguments = c.Argument("path", EnglishLocalisation.DirectoryMoveOptionNote, true);
                c.OnExecute(() =>
                {
                    using (var scope = _container.BeginLifetimeScope())
                    {
                        container.Resolve<DirectoryController>()
                        .Remove(arguments.Values[0]);
                    }
                    return 0;
                });
            });
        }
    }
}