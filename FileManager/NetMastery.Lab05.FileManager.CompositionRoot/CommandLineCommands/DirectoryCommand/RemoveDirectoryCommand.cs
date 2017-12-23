using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;


namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands.DirectoryCommands
{
    public class RemoveDirectoryCommand : CommandLineCommand
    {
        public RemoveDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.RemoveCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryMoveOptionNote, true);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    container.Resolve<DirectoryController>()
                    .Remove(arguments.Values[0]);
                }
                return 0;
            });
        }
    }
}