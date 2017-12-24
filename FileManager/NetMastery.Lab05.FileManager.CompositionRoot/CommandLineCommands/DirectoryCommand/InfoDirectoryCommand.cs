using Autofac;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    public class InfoDirectoryCommand : CommandLine
    {
        public InfoDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.InfoCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, false);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                   container.Resolve<DirectoryController>().GetDirectoryInfo(arguments.Values[arguments.Values.Count-1]); 
                }
                return 0;
            });

        }
    }
}
