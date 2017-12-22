using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.ViewModels;
using NetMastery.Lab05.FileManagerCompositionRoot;

namespace CompositionRoot.CommandLineCommands
{
    public class AddDirectoryCommand : CommandLineCommand
    {
        public AddDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.DirectoryCommand;

            Command(CommandLineNames.CreateOption, c =>
            {
                var arguments = c.Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, true);
                c.OnExecute(() =>
                {
                    using (var scope = _container.BeginLifetimeScope())
                    {
                        container.Resolve<DirectoryController>()
                        .Add(arguments.Values[0], arguments.Values[1]);
                    }
                    return 0;
                });
            }); 
        }
    }
}
