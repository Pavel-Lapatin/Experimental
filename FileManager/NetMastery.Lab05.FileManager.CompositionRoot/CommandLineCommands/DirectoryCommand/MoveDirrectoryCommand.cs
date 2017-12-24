using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;


namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class MoveDirectoryCommand : CommandLine
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
                    .Move(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                }
                arguments.Values.Clear();
                return 0;
            });
        }
    }
}