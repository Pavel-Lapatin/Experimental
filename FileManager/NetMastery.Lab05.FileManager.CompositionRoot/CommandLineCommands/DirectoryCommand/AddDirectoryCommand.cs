using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;
using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands.DirectoryCommands
{
    public class AddDirectoryCommand : CommandLineCommand
    {
        public AddDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.CreateCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, true);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    container.Resolve<DirectoryController>()
                    .Add(arguments.Values[0], arguments.Values[1]);
                }
                return 0;
            });
        }
    }
}
