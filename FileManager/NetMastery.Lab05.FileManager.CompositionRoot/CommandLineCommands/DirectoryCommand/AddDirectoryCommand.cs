using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.CompositionRoot;
using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class AddDirectoryCommand : CommandLine
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
                    .Add(arguments.Values[arguments.Values.Count-2], arguments.Values[arguments.Values.Count-1]);
                }
                arguments.Values.Clear();
                return 0;
            });
        }
    }
}
