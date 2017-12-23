using Autofac;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands.DirectoryCommands
{
    public class InfoDirectoryCommand : CommandLineCommand
    {
        public InfoDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.InfoCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, false);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                   container.Resolve<DirectoryController>().GetDirectoryInfo(arguments.Value); 
                }
                return 0;
            });

        }
    }
}
