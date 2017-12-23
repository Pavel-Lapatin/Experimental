using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;


namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands.DirectoryCommands
{
    public class MoveDirectoryCommand : CommandLineCommand
    {
        public MoveDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.MoveCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryMoveOptionNote, true);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    container.Resolve<DirectoryController>()
                    .Move(arguments.Values[0], arguments.Values[1]);
                }
                return 0;
            });
        }
    }
}