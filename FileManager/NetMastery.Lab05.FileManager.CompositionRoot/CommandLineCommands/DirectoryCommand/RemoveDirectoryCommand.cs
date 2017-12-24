using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;


namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class RemoveDirectoryCommand : CommandLine
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
                    .Remove(arguments.Values[arguments.Values.Count - 1]);
                }
                arguments.Values.Clear();
                return 0;
            });
        }
    }
}